namespace CoreWCFService1
{
    public class ServerHost
    {
        public ServerHost() { }

        public void Start()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddServiceModelServices();

            var app = builder.Build();

            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<Service>();
                serviceBuilder.AddServiceEndpoint<Service, IService>(new NetHttpBinding(), "/Service.svc");
            });

            app.Run();
        }
    }
}
