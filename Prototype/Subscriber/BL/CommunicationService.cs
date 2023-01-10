using Grpc.Core;
using Prototype.Subscriber.Contract;
using Prototype.Subscriber.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.BL
{
    internal class CommunicationService : ICommunicationService
    {
        private Server _server;

        public void StartHostServer(SubscribeConfig subscribeConfig)
        {
            _server = new Server()
            {
                Services = { SubscriberGrpcService.BindService(new SubscriberService()) },
                Ports = { new ServerPort(subscribeConfig.IpAdress, subscribeConfig.PortNumber, ServerCredentials.Insecure) }
            };

            _server.Start();
        }

        public async Task ShutDownAsync()
        {
            await _server.ShutdownAsync();
        }

        public bool Subscribe(ISubscribeConfig subscribeConfig)
        {
            throw new NotImplementedException();
        }
    }
}
