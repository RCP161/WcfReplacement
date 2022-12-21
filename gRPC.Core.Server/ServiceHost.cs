using Grpc.Core;

namespace gRPC.Core.Server
{
    internal class ServiceHost
    {
        public ServiceHost()
        {
            m_server = new Grpc.Core.Server()
            {
                Services =
                {
                    Lib.EmployeeService.BindService(new EmployeeService())
                },
                Ports = { new ServerPort("localhost", 11111, ServerCredentials.Insecure) }
            };
        }

        public void Start()
        {
            m_server.Start();
        }

        public async Task ShutDownAsync()
        {
            await m_server.ShutdownAsync();
        }

        private readonly Grpc.Core.Server m_server;
    }
}
