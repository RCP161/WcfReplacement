

using gRPC.Core.Lib;
using Grpc.Core;

var result = GetAllEmployee().GetAwaiter().GetResult();

Console.WriteLine($"Request was successfull: {result.Success}");
Console.WriteLine($"{result.Employees.Count} employees found");
Console.WriteLine();
Console.WriteLine();

foreach (var emp in result.Employees)
{
    Console.WriteLine("======================");
    Console.WriteLine();
    Console.WriteLine(emp.Name);
    Console.WriteLine(emp.Email);
    Console.WriteLine(emp.DateTimeStamp);
    Console.WriteLine(emp.Skill);
}

Console.WriteLine("Press any key to stop ...");


static async Task<ResponseMessage> GetAllEmployee()
{
    var channel = new Grpc.Core.Channel("localhost", 11111, ChannelCredentials.Insecure);

    var client = new EmployeeService.EmployeeServiceClient(channel);

    return await client.GetAllEmployeeAsync(new Google.Protobuf.WellKnownTypes.Empty());
}