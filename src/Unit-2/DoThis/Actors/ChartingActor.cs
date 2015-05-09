using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization.Configuration;
using Akka.Actor;

namespace ChartApp.Actors
{
    public class ChartingActor : ReceiveActor
    {

        #region Messages

        public class InitializeChart
        {
            public InitializeChart(Dictionary<string, Series> initialSeries)
            {
                InitialSeries = initialSeries;
            }

            public Dictionary<string, Series> InitialSeries { get; private set; }
        }

        public class AddSeries
        {
            public AddSeries(Series series)
            {
                Series = series;
            }

            public Series Series { get; private set; }
        }

        public class RemoveSeries
        {
            public RemoveSeries(string seriesName)
            {
                SeriesName = seriesName;
            }

            public string SeriesName { get; private set; }
        }

        #endregion

        /// <summary>
        /// Maximum number of points we will allow in a series
        /// </summary>
        public const int MaxPoints = 250;

        /// <summary>
        /// Incrementing counter we use to plot along the x-axis
        /// </summary>
        private int _xPosCounter = 0;

        private readonly Chart _chart;
        private Dictionary<string, Series> _seriesIndex;

        public ChartingActor(Chart chart) : this(chart, new Dictionary<string, Series>())
        {
        }

        public ChartingActor(Chart chart, Dictionary<string, Series> seriesIndex)
        {
            _chart = chart;
            _seriesIndex = seriesIndex;

            Receive<InitializeChart>(ic => _HandleInitialize(ic));
            Receive<AddSeries>(addSeries => _HandleAddSeries(addSeries));
            Receive<RemoveSeries>(removeSeries => _HandleRemoveSeries(removeSeries));
            Receive<Metric>(metric => _HandleMetrics(metric));
        }

        #region Individual Message Type Handlers

        private void _HandleInitialize(InitializeChart ic)
        {
            if (ic.InitialSeries != null)
            {
                // swap the two series out
                _seriesIndex = ic.InitialSeries;
            }

            // delete any existing series
            _chart.Series.Clear();

            // set up the axes
            var area = _chart.ChartAreas[0];
            area.AxisX.IntervalType = DateTimeIntervalType.Number;
            area.AxisY.IntervalType = DateTimeIntervalType.Number;

            _SetChartBoundaries();

            // attempt to render the initial chart
            if (_seriesIndex.Any())
            {
                foreach (var series in _seriesIndex)
                {
                    // force both the chart and the interval index to use the same names
                    series.Value.Name = series.Key;
                    _chart.Series.Add(series.Value);
                }
            }

            _SetChartBoundaries();
        }

        private void _HandleAddSeries(AddSeries series)
        {
            if (string.IsNullOrEmpty(series.Series.Name) || _seriesIndex.ContainsKey(series.Series.Name)) return;

            _seriesIndex.Add(series.Series.Name, series.Series);
            _chart.Series.Add(series.Series);
            _SetChartBoundaries();
        }

        private void _HandleRemoveSeries(RemoveSeries series)
        {
            if (string.IsNullOrEmpty(series.SeriesName) || !_seriesIndex.ContainsKey(series.SeriesName)) return;

            var seriesToRemove = _seriesIndex[series.SeriesName];
            _seriesIndex.Remove(series.SeriesName);
            _chart.Series.Remove(seriesToRemove);
            _SetChartBoundaries();
        }

        private void _HandleMetrics(Metric metric)
        {
            if (string.IsNullOrEmpty(metric.Series) || !_seriesIndex.ContainsKey(metric.Series)) return;

            var series = _seriesIndex[metric.Series];
            if (series.Points == null)
                return;

            series.Points.AddXY(_xPosCounter++, metric.CounterValue);
            while(series.Points.Count > MaxPoints) series.Points.RemoveAt(0);
            _SetChartBoundaries();
        }

        #endregion

        private void _SetChartBoundaries()
        {
            double maxAxisX, maxAxisY, minAxisX, minAxisY = 0.0d;

            var allPoints = _seriesIndex.Values.Aggregate(new HashSet<DataPoint>(),
                (set, series) => new HashSet<DataPoint>(set.Concat(series.Points)));

            var yValues = allPoints.Aggregate(new List<double>(), (list, point) => list.Concat(point.YValues).ToList());

            maxAxisX = _xPosCounter;
            minAxisX = _xPosCounter - MaxPoints;
            maxAxisY = yValues.Count > 0 ? Math.Ceiling(yValues.Max()) : 1.0d;
            minAxisY = yValues.Count > 0 ? Math.Floor(yValues.Min()) : 0.0d;

            if (allPoints.Count > 2)
            {
                var area = _chart.ChartAreas[0];
                area.AxisX.Minimum = minAxisX;
                area.AxisX.Maximum = maxAxisX;
                area.AxisY.Minimum = minAxisY;
                area.AxisY.Maximum = maxAxisY;
            }
        }
    }
}
