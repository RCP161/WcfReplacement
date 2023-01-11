using Catel.IoC;
using Prototype.Publisher.Contract;
using Prototype.Publisher.Core;
using Prototype.Publisher.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void StartServer()
        {
            _communicationService.StartServiceHost(_localServerConfig);
        }

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
    }
}
