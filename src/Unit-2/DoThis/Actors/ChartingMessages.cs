using Akka.Actor;

namespace ChartApp.Actors
{
    /// <summary>
    /// Signal used to indicate that it's time to sample all counters
    /// </summary>
    public class GatherMetrics
    {
    }

    /// <summary>
    /// Metric data at the time of sample
    /// </summary>
    public class Metric
    {
        public Metric(string series, float counterValue)
        {
            Series = series;
            CounterValue = counterValue;
        }

        public string Series { get; private set; }

        public float CounterValue { get; private set; }
    }

    public enum CounterType
    {
        Cpu,
        Memory,
        Disk
    }

    /// <summary>
    /// Enables a counter and begins publishing values to <see cref="Subscriber"/>
    /// </summary>
    public class SubscribeCounter
    {
        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Counter = counter;
            Subscriber = subscriber;
        }

        public CounterType Counter { get; private set; }

        public IActorRef Subscriber { get; private set; }
    }

    /// <summary>
    /// Enables a counter and begins publishing values to <see cref="Subscriber"/>
    /// </summary>
    public class UnsubscribeCounter
    {
        public UnsubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Counter = counter;
            Subscriber = subscriber;
        }

        public CounterType Counter { get; private set; }

        public IActorRef Subscriber { get; private set; }
    }
}
