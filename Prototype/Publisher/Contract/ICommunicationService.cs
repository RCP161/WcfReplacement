﻿using Prototype.Testing.Core;
using System.Threading.Tasks;

namespace Prototype.Publisher.Contract
{
    internal interface ICommunicationService
    {
        void StartServiceHost(IServerConfig localServerConfig);
        Task StopServiceHostAsync();


        bool SendPresentStandad(byte[] data, int dataSize);
        bool SendRequestPerformance(byte[] data, int dataSize);
        bool SendSerialisationBinaryPerformance(byte[] data, int size, int deep);
        bool SendSerialisationProtoPerformance(SerialisationTestObj serialisationTestObj, int size, int deep);

        event Events.SubscriberEventHandler SubscriberEvent;
    }
}
