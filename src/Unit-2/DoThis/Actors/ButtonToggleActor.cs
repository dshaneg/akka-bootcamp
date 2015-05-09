using System.Windows.Forms;
using Akka.Actor;

namespace ChartApp.Actors
{
    /// <summary>
    /// Actor responsible for managing button toggles
    /// </summary>
    public class ButtonToggleActor : UntypedActor
    {
        /// <summary>
        /// Toggles this button on or off and sends an appropriate message
        /// to the <see cref="PerformanceCounterCoordinatorActor"/>
        /// </summary>
        public class Toggle { }

        private readonly CounterType _myCounterType;
        private bool _isToggledOn;
        private readonly Button _myButton;
        private readonly IActorRef _coordinatorActor;

        public ButtonToggleActor(IActorRef coordinatorActor, Button myButton, CounterType myCounterType, bool isToggledOn = false)
        {
            _coordinatorActor = coordinatorActor;
            _myButton = myButton;
            _myCounterType = myCounterType;
            _isToggledOn = isToggledOn;
        }

        protected override void OnReceive(object message)
        {
            if (message is Toggle && _isToggledOn)
            {
                // toggle is currently on

                // stop watching this counter
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Unwatch(_myCounterType));

                _Toggle();
            }
            else if (message is Toggle && !_isToggledOn)
            {
                // toggle is currently off

                // start watching this counter
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Watch(_myCounterType));

                _Toggle();
            }
            else
            {
                Unhandled(message);
            }
        }

        private void _Toggle()
        {
            _isToggledOn = !_isToggledOn;

            _myButton.Text = string.Format("{0} ({1})", 
                _myCounterType.ToString().ToUpperInvariant(), 
                _isToggledOn ? "ON" : "OFF");
        }
    }
}
