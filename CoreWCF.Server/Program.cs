using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCF.Server;

var builder = NetApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.AllowSynchronousIO = true;
});

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();
app.UseServiceModel(builder =>
{
    builder.AddService((serviceOptions) => { })
    // Add a BasicHttpBinding at a specific endpoint
    .AddServiceEndpoint<EchoService, IEchoService>(new NetTcpBinding(), "/EchoService")
});
var serviceMetadataBehavior = app.Services.GetRequiredService();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();