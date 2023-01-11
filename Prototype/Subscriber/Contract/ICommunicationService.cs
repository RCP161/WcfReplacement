﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.Contract
{
    internal interface ICommunicationService
    {
        void StartServiceHost(IServerConfig localServerConfig);
        Task StopServiceHostAsync();
        bool Subscribe(IServerConfig publisherServerConfig);
        bool Unsubscribe(IServerConfig publisherServerConfig);
    }
}
