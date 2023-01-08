using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    internal interface IAppConfig
    {
        string IpAdress { get; }
        int PortNumber { get; }
        AppType AppType { get; }
    }
}
