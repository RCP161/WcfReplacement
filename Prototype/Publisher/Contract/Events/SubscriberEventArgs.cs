using System;

namespace Prototype.Publisher.Contract.Events
{
    internal class SubscriberEventArgs : EventArgs
    {
        public IServerConfig ServerConfig { get; set; }
        public bool Subscribed { get; set; }
    }
}
