using System.Threading.Tasks;

namespace Prototype.Subscriber.Contract
{
    internal interface ICommunicationService
    {
        void StartServiceHost(IServerConfig localServerConfig);
        Task StopServiceHostAsync();
        bool Subscribe(IServerConfig publisherServerConfig);
        bool Unsubscribe(IServerConfig publisherServerConfig);

        event Events.DataReceivedEventHandler DataReceivedEvent;
    }
}
