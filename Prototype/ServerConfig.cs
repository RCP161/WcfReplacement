using Prototype.Subscriber.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    internal class ServerConfig : IServerConfig
    {
        public string IpAdress { get; internal set; }
        public int PortNumber { get; internal set; }

        public override string ToString()
        {
            return $"{IpAdress}:{PortNumber}";
        }
    }
}
