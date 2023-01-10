using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class PublisherService : PublisherGrpcService.PublisherGrpcServiceBase
    {
        public override Task<ResponseMessage> Register(SubscriberModel request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}
