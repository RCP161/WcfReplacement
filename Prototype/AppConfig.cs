using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    internal class AppConfig : IAppConfig
    {
        public AppConfig(AppType appType, int portNumber)
        {
            AppType = appType;
            PortNumber = portNumber;
        }

        public string IpAdress { get; } = Constants.LocalHost;
        public int PortNumber { get; private set; }
        public AppType AppType { get; private set; }
    }
}
