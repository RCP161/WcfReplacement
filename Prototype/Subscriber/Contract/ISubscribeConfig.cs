using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Contract
{
    internal interface ISubscribeConfig
    {
        string IpAdress { get; }
        int PortNumber { get; }
        string PublisherIpAdress { get; }
        int PublisherPortNumber { get; }
    }
}
