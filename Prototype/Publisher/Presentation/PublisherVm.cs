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
        private readonly PublisherM _model;

        public PublisherVm(int portNumber)
        {
        }

        internal PublisherVm()
        {
            _model = new PublisherM();
        }

        internal void Start()
        {
            // Bl staren und Event Registrieren?
            throw new NotImplementedException();
        }

        private void AskForScenario()
        {
            Scenario? scenario = null;
            while (scenario == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the scenario number:");

                scenario = ReadScenarioNumber();
            }

            //_model.PublisherPort = scenario.Value;
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
