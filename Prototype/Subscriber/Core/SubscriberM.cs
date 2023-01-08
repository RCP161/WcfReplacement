using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Core
{
    internal class SubscriberM
    {
        internal string PublisherIpAdress { get; } = Constants.LocalHost;

        internal int PublisherPort { get; set; }
    }
}
