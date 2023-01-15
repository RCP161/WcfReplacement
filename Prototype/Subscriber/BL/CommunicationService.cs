using Grpc.Core;
using Prototype.Logging.Contract;
using Prototype.Publisher.BL;
using Prototype.Subscriber.Contract;
using Prototype.Subscriber.Contract.Events;
using Prototype.Testing.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Subscriber.BL
{
    internal class CommunicationService : ICommunicationService, IDisposable
    {
        private readonly ITestDataService _testDataService;
        private readonly ILog _log;
        private IServerConfig _localServerConfig;
        private SubscriberService _subscriberService;
        private Server _server;

        public CommunicationService(ITestDataService testDataService, ILog log)
        {
            _testDataService = testDataService;
            _log = log;
        }

        public void StartServiceHost(IServerConfig localServerConfig)
        {
            _localServerConfig = localServerConfig;

            _subscriberService = new SubscriberService(_testDataService, _log);
            _subscriberService.DataReceivedEvent += SubscriberService_DataReceivedEvent;

            var options = new List<ChannelOption>()
            {
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength, Constants.MaxGrpcMessageSize)
            };

            _server = new Server(options)
            {
                Services = { SubscriberGrpcService.BindService(_subscriberService) },
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
            catch(Exception ex)
            {
                _log.Log(ex);
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
            catch(Exception ex)
            {
                _log.Log(ex);
                return false;
            }
        }

        public event DataReceivedEventHandler DataReceivedEvent;

        private void SubscriberService_DataReceivedEvent(object sender, DataReceivedEventArgs e)
        {
            DataReceivedEvent?.Invoke(sender, e);
        }

        public void Dispose()
        {
            if(_subscriberService != null)
                _subscriberService.DataReceivedEvent -= SubscriberService_DataReceivedEvent;
        }
    }
}
