using Grpc.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class PublisherService : PublisherGrpcService.PublisherGrpcServiceBase
    {
        private readonly List<IServerConfig> _subscribers;

        public PublisherService(List<IServerConfig> subscribers)
        {
            _subscribers = subscribers;
        }

        public override Task<ResponseMessage> Subscribe(SubscriberModel request, ServerCallContext context)
        {
            if (!_subscribers.Any(x => x.IpAdress == request.IpAddress && x.PortNumber == request.PortNumber))
            {
                var subscriber = new ServerConfig()
                {
                    IpAdress = request.IpAddress,
                    PortNumber = request.PortNumber
                };

                _subscribers.Add(subscriber);
            }

            var message = new ResponseMessage()
            {
                Message = "Subscribed",
                Success = true,
            };

            return Task.FromResult(message);
        }

        public override Task<ResponseMessage> UnSubscribe(SubscriberModel request, ServerCallContext context)
        {
            var subscriber = _subscribers.SingleOrDefault(x => x.IpAdress == request.IpAddress && x.PortNumber == request.PortNumber);

            if (subscriber != null)
                _subscribers.Remove(subscriber);

            var message = new ResponseMessage()
            {
                Message = "Unsubscribed",
                Success = true,
            };

            return Task.FromResult(message);
        }
    }
}
