using System;

namespace Prototype.Subscriber.Contract.Events
{
    internal class DataReceivedEventArgs : EventArgs
    {
        public string ScenarioName { get; set; }
    }
}
