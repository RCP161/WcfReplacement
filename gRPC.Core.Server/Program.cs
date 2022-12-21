


using gRPC.Core.Server;

ServiceHost host = new ServiceHost();
host.Start();

Console.WriteLine("Server is listening ...");
Console.WriteLine("Press any key to stop the server ...");
Console.ReadKey();