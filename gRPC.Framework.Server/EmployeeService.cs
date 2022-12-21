using Google.Protobuf.WellKnownTypes;
using gRPC.Framework.Lib;
using Grpc.Core;
using System.Threading.Tasks;

namespace gRPC.Framework.Server
{
    internal class EmployeeService : Lib.EmployeeService.EmployeeServiceBase
    {
        public override Task<ResponseMessage> GetAllEmployee(Empty request, ServerCallContext context)
        {
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
    }
}