using Grpc.Core;
using Prototype.Publisher.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class CommunicationService : ICommunicationService
    {
        private Server _server;
        private List<IServerConfig> _subscribers;

        public CommunicationService()
        {
            _subscribers = new List<IServerConfig>();
        }

        public void StartServiceHost(IServerConfig localServerConfig)
        {
            _server = new Server()
            {
                Services = { PublisherGrpcService.BindService(new PublisherService(_subscribers)) },
                Ports = { new ServerPort(localServerConfig.IpAdress, localServerConfig.PortNumber, ServerCredentials.Insecure) }
            };

            _server.Start();
        }

        public async Task StopServiceHostAsync()
        {
            await _server.ShutdownAsync();
        }
    }
}
