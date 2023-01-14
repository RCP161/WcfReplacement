using Grpc.Core;
using Prototype.Publisher.BL;
using Prototype.Subscriber.Contract;
using Prototype.Testing.Contract;
using System.Threading.Tasks;

namespace Prototype.Subscriber.BL
{
    internal class CommunicationService : ICommunicationService
    {
        private readonly ITestDataService _testDataService;
        private IServerConfig _localServerConfig;
        private Server _server;

        public CommunicationService(ITestDataService testDataService)
        {
            _testDataService = testDataService;
        }


        public void StartServiceHost(IServerConfig localServerConfig)
        {
            _localServerConfig = localServerConfig;

            _server = new Server()
            {
                Services = { SubscriberGrpcService.BindService(new SubscriberService(_testDataService)) },
                Ports = { new ServerPort(localServerConfig.IpAdress, localServerConfig.PortNumber, ServerCredentials.Insecure) }
            };

            _server.Start();
        }

        public async Task StopServiceHostAsync()
        {
            await _server.ShutdownAsync();
        }

        public bool Subscribe(IServerConfig publisherServerConfig)
        {
            var channel = new Channel(publisherServerConfig.IpAdress, publisherServerConfig.PortNumber, ChannelCredentials.Insecure);
            var client = new PublisherGrpcService.PublisherGrpcServiceClient(channel);

            var subscriberModel = new SubscriberModel()
            {
                IpAddress = _localServerConfig.IpAdress,
                PortNumber = _localServerConfig.PortNumber
            };

            try
            {
                return client.Subscribe(subscriberModel).Successful;
            }
            catch
            {
                // Log
                return false;
            }
        }

        public bool Unsubscribe(IServerConfig publisherServerConfig)
        {
            var channel = new Channel(publisherServerConfig.IpAdress, publisherServerConfig.PortNumber, ChannelCredentials.Insecure);
            var client = new PublisherGrpcService.PublisherGrpcServiceClient(channel);

            var subscriberModel = new SubscriberModel()
            {
                IpAddress = _localServerConfig.IpAdress,
                PortNumber = _localServerConfig.PortNumber
            };

            try
            {
                return client.Unsubscribe(subscriberModel).Successful;
            }
            catch
            {
                // Log
                return false;
            }
        }
    }
}
