using Grpc.Core;
using Prototype.Publisher.Contract.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class PublisherService : PublisherGrpcService.PublisherGrpcServiceBase
    {
        private readonly Dictionary<ServerConfig, Channel> _subscribers;
        private readonly IServerConfig _localServerConfig;

        public PublisherService(Dictionary<ServerConfig, Channel> subscribers, IServerConfig localServerConfig)
        {
            _subscribers = subscribers;
            _localServerConfig = localServerConfig;
        }

        public override Task<ResponseMessage> Subscribe(SubscriberModel request, ServerCallContext context)
        {
            if(!_subscribers.Keys.Any(x => x.IpAdress == request.IpAddress && x.PortNumber == request.PortNumber))
            {
                var serverConfig = new ServerConfig()
                {
                    IpAdress = request.IpAddress,
                    PortNumber = request.PortNumber
                };

                var channel = new Channel(serverConfig.IpAdress, serverConfig.PortNumber, ChannelCredentials.Insecure);

                _subscribers.Add(serverConfig, channel);

                RaiseSubscriberEvent(serverConfig, true);
            }

            var message = new ResponseMessage()
            {
                Message = "Subscribed",
                Successful = true,
            };

            return Task.FromResult(message);
        }

        public override Task<ResponseMessage> Unsubscribe(SubscriberModel request, ServerCallContext context)
        {
            var subscriber = _subscribers.Keys.SingleOrDefault(x => x.IpAdress == request.IpAddress && x.PortNumber == request.PortNumber);

            if(subscriber != null)
            {
                _subscribers.Remove(subscriber);
                RaiseSubscriberEvent(subscriber, false);
            }

            var message = new ResponseMessage()
            {
                Message = "Unsubscribed",
                Successful = true,
            };

            return Task.FromResult(message);
        }


        public event SubscriberEventHandler SubscriberEvent;

        private void RaiseSubscriberEvent(IServerConfig serverConfig, bool subscribed)
        {
            var args = new SubscriberEventArgs()
            {
                ServerConfig = serverConfig,
                Subscribed = subscribed
            };

            SubscriberEvent?.Invoke(this, args);
        }
    }
}
