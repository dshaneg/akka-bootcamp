using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Akka.Actor;
using ChartApp.Actors;

namespace ChartApp
{
    public partial class Main : Form
    {
        private IActorRef _chartActor;
        //private readonly AtomicCounter _seriesCounter = new AtomicCounter(1);

        private IActorRef _coordinatingActor;
        private readonly Dictionary<CounterType, IActorRef> _toggleActors = new Dictionary<CounterType, IActorRef>(); 

        public Main()
        {
            InitializeComponent();
        }

        #region Initialization


        private void Main_Load(object sender, EventArgs e)
        {
            _chartActor = Program.ChartActors.ActorOf(
                Props.Create(() => new ChartingActor(sysChart, buttonPauseResume)), "charting");
            _chartActor.Tell(new ChartingActor.InitializeChart(null));

            _coordinatingActor = Program.ChartActors.ActorOf(Props.Create(() => 
                new PerformanceCounterCoordinatorActor(_chartActor)), "counters");

            // CPU button toggle actor
            _toggleActors[CounterType.Cpu] =
                Program.ChartActors.ActorOf(
                    Props.Create(() => new ButtonToggleActor(_coordinatingActor, buttonCpu, CounterType.Cpu, false))
                        .WithDispatcher("akka.actor.synchronized-dispatcher"));

            // MEMORY button toggle actor
            _toggleActors[CounterType.Memory] =
                Program.ChartActors.ActorOf(
                    Props.Create(() => new ButtonToggleActor(_coordinatingActor, buttonMemory, CounterType.Memory, false))
                        .WithDispatcher("akka.actor.synchronized-dispatcher"));

            // DISK button toggle actor
            _toggleActors[CounterType.Disk] =
                Program.ChartActors.ActorOf(
                    Props.Create(() => new ButtonToggleActor(_coordinatingActor, buttonDisk, CounterType.Disk, false))
                        .WithDispatcher("akka.actor.synchronized-dispatcher"));

            // set the CPU toggle to ON so we start getting some data
            _toggleActors[CounterType.Cpu].Tell(new ButtonToggleActor.Toggle());
        }

        #endregion

        private void buttonCpu_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Cpu].Tell(new ButtonToggleActor.Toggle());
        }

        private void buttonMemory_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Memory].Tell(new ButtonToggleActor.Toggle());
        }

        private void buttonDisk_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Disk].Tell(new ButtonToggleActor.Toggle());
        }

        private void buttonPauseResume_Click(object sender, EventArgs e)
        {
            _chartActor.Tell(new ChartingActor.TogglePause());
        }
    }
}
