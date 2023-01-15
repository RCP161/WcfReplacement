using System;

namespace Prototype.Subscriber.Contract.Events
{
    internal class DataRecievedEventArgs : EventArgs
    {
        public string ScenarioName { get; set; }
    }
}
