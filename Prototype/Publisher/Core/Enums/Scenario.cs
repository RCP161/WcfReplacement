using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Core.Enums
{
    internal enum Scenario
    {
        /// <summary>
        /// Ends the server
        /// </summary>
        Stop = 1,
        /// <summary>
        /// Request, 1s, 64 kb
        /// </summary>
        PresentStandard,
        /// <summary>
        /// 0.5s, 5mb
        /// </summary>
        RequestVsStreamPerformance,
        /// <summary>
        /// Xml/Byte/Proto, 1s, 5mb
        /// </summary>
        SerialisationPerformance
    }
}
