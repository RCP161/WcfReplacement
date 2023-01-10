using Prototype.Subscriber.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Core
{
    internal class SubscribeConfig : ISubscribeConfig
    {
        public string IpAdress { get; } = Constants.LocalHost;
        public int PortNumber { get; internal set; }
        public string PublisherIpAdress { get; } = Constants.LocalHost;
        public int PublisherPortNumber { get; internal set; }
    }
}
