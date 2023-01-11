using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Contract
{
    internal interface ICommunicationService
    {
        void StartServiceHost(IServerConfig localServerConfig);
        Task StopServiceHostAsync();
    }
}
