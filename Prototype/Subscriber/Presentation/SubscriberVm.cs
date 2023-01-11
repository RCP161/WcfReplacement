using Catel.IoC;
using Prototype.Subscriber.Contract;
using System;

namespace Prototype.Subscriber.Presentation
{
    internal class SubscriberVm
    {
        private readonly ICommunicationService _communicationService;
        private readonly IServerConfig _localServerConfig;

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
        }

        #region Subscribe

        private void SubscribeOnPublisher()
        {
            var subscribed = false;

            while (!subscribed)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the port of the publisher port number:");

                subscribed = TrySubscibtion();
            }
        }

        private bool TrySubscibtion()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if (isNumber && portNumber <= 65535)
            {
                var publisherConfig = new ServerConfig()
                {
                    IpAdress = Constants.LocalHost,
                    PortNumber = portNumber
                };

                if (_communicationService.Subscribe(publisherConfig))
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
        }
    }
}
