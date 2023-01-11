using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Contract.Events
{
    internal class SubscriberEventArgs : EventArgs
    {
        public IServerConfig ServerConfig { get; set; }
        public bool Subscribed { get; set; }
    }
}
