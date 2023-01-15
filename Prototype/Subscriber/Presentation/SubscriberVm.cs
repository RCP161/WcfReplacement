using Catel.IoC;
using Prototype.Subscriber.Contract;
using System;

namespace Prototype.Subscriber.Presentation
{
    internal class SubscriberVm
    {
        private readonly ICommunicationService _communicationService;
        private readonly IServerConfig _localServerConfig;
        private IServerConfig _publisherServerConfig;

        public SubscriberVm(IServerConfig serverConfig)
        {
            _localServerConfig = serverConfig;
            _communicationService = ServiceLocator.Default.ResolveType<ICommunicationService>();
        }

        internal void Start()
        {
            StartServer();
            SubscribeOnPublisher();
            WaitForStop();
        }

        private void StartServer()
        {
            _communicationService.StartServiceHost(_localServerConfig);
            _communicationService.DataReceivedEvent += CommunicationService_DataReceivedEvent;
        }

        private void CommunicationService_DataReceivedEvent(object sender, Contract.Events.DataReceivedEventArgs e)
        {
            Console.WriteLine($"{Constants.InfoPrefix}Data received fro {e.ScenarioName}");
        }

        #region Subscribe

        private void SubscribeOnPublisher()
        {
            var subscribed = false;

            while(!subscribed)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the port of the publisher port number:");
                Console.WriteLine();

                subscribed = TrySubscribtion();
            }
        }

        private bool TrySubscribtion()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if(isNumber && portNumber <= 65535)
            {
                _publisherServerConfig = new ServerConfig()
                {
                    IpAdress = Constants.LocalHost,
                    PortNumber = portNumber
                };

                if(_communicationService.Subscribe(_publisherServerConfig))
                    return true;
            }

            return false;
        }

        #endregion

        private void WaitForStop()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.WriteLine();
            Console.WriteLine("Server is listening ...");
            Console.WriteLine("Press any key to stop the server ...");
            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.ReadKey();

            _communicationService.Unsubscribe(_publisherServerConfig);
            _communicationService.StopServiceHostAsync().GetAwaiter().GetResult();
        }
    }
}
