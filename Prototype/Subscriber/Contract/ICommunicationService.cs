using Prototype.Subscriber.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Contract
{
    internal interface ICommunicationService
    {
        void StartHostServer(SubscribeConfig subscribeConfig);
        bool Subscribe(ISubscribeConfig subscribeConfig);
    }
}
