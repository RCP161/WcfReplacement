using Grpc.Core;
using Prototype.Publisher.Contract;
using Prototype.Publisher.Contract.Events;
using Prototype.Subscriber.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class CommunicationService : ICommunicationService
    {
        private Server _server;
        private readonly Dictionary<ServerConfig, Channel> _subscribers;

        public CommunicationService()
        {
            _subscribers = new Dictionary<ServerConfig, Channel>();
        }

        public void StartServiceHost(IServerConfig localServerConfig)
        {
            var publisherService = new PublisherService(_subscribers, localServerConfig);
            publisherService.SubscriberEvent += PublisherService_SubscriberEvent;

            _server = new Server()
            {
                Services = { PublisherGrpcService.BindService(publisherService) },
                Ports = { new ServerPort(localServerConfig.IpAdress, localServerConfig.PortNumber, ServerCredentials.Insecure) }
            };

            _server.Start();
        }

        public async Task StopServiceHostAsync()
        {
            foreach (var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    client.Unsubscribed(new Google.Protobuf.WellKnownTypes.Empty());
                }
                catch
                {
                    // Log
                }

            }

            await _server.ShutdownAsync();
        }


        public event SubscriberEventHandler SubscriberEvent;

        private void PublisherService_SubscriberEvent(object sender, Contract.Events.SubscriberEventArgs e)
        {
            SubscriberEvent?.Invoke(sender, e);
        }
    }
}
