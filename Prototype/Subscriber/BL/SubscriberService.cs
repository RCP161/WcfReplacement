using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Subscriber.BL
{
    internal class SubscriberService : SubscriberGrpcService.SubscriberGrpcServiceBase
    {
        public override Task<ResponseMessage> PresentStandard(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
            var employees = new Google.Protobuf.Collections.RepeatedField<EmployeeModel>();

            for (int i = 0; i < 5; i++)
            {

                employees.Add(
                    new EmployeeModel
                    {
                        DateTimeStamp = new Timestamp(),
                        Name = $"user{i}",
                        Email = $"user{i}@test.de",
                        Skill = $"{i} skills"
                    });
            }


            var message = new ResponseMessage()
            {
                Message = "finished",
                Success = employees.Count > 0
            };

            message.Employees.Add(employees);

            return Task.FromResult(message);
        }

        public override Task<ResponseMessage> RequestPerformance(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<ResponseMessage> SerialisationBinaryPerformance(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<ResponseMessage> SerialisationProtoPerformance(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        public override Task<ResponseMessage> SerialisationXmlPerformance(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<ResponseMessage> StreamPerformance(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<Empty> Unsubscribed(Empty request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}