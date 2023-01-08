using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Core.Enums
{
    internal enum Scenario
    {
        PresentStandard, // Request, 1s, 64 kb
        RequestVsStreamPerformance, // 0.5s, 5mb
        SerialisationPerformance // Xml/Byte/Proto, 1s, 5mb
    }
}
