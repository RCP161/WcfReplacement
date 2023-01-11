using Catel.IoC;
using Prototype.Publisher.Contract;
using Prototype.Publisher.Core.Enums;
using System;

namespace Prototype.Publisher.Presentation
{
    internal class PublisherVm
    {
        private readonly ServerConfig _localServerConfig;
        private readonly ICommunicationService _communicationService;

        public PublisherVm(ServerConfig config)
        {
            _localServerConfig = config;
            _communicationService = ServiceLocator.Default.ResolveType<ICommunicationService>();
        }

        internal void Start()
        {
            StartServer();
            AskForScenario();
        }

        #region Server

        private void StartServer()
        {
            _communicationService.StartServiceHost(_localServerConfig);
            _communicationService.SubscriberEvent += CommunicationService_SubscriberEvent;
        }

        private void CommunicationService_SubscriberEvent(object sender, Contract.Events.SubscriberEventArgs e)
        {
            if (e.Subscribed)
                Console.WriteLine($"-- {e.ServerConfig} subscribed");
            else
                Console.WriteLine($"-- {e.ServerConfig} unsubscribed");
        }

        #endregion

        #region Scenario

        private void AskForScenario()
        {
            Scenario? scenario = null;
            while (scenario == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the scenario number:");

                foreach (var sc in Enum.GetValues(typeof(Scenario)))
                {
                    Console.WriteLine($"{(int)sc} - {sc}");
                }
                Console.WriteLine();

                scenario = ReadScenarioNumber();
            }
        }

        private Scenario? ReadScenarioNumber()
        {
            var portText = Console.ReadLine();
            var isScenario = Enum.TryParse<Scenario>(portText, out Scenario scenario);

            if (!isScenario)
                return null;

            return scenario;
        }

        #endregion

    }
}
