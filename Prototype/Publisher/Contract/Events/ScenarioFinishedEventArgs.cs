using System;

namespace Prototype.Publisher.Contract.Events
{
    internal class ScenarioFinishedEventArgs : EventArgs
    {
        public string Text { get; set; }
        public bool Successful { get; set; }
        public double AverageExecutionTime { get; set; }
    }
}
