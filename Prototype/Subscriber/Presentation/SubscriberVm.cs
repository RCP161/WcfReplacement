using Catel.IoC;
using Prototype.Subscriber.Contract;
using Prototype.Subscriber.Core;
using System;

namespace Prototype.Subscriber.Presentation
{
    internal class SubscriberVm
    {
        private readonly SubscribeConfig _subscribeConfig;
        private readonly ICommunicationService _communicationService;

        public SubscriberVm(int portNumber)
        {
            _subscribeConfig = new SubscribeConfig()
            {
                PortNumber = portNumber
            };

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
            _communicationService.StartHostServer(_subscribeConfig);
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
                _subscribeConfig.PublisherPortNumber = portNumber;

                if (_communicationService.Subscribe(_subscribeConfig))
                    return true;
            }

            return false;
        }

        #endregion

        private void WaitForStop()
        {
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
