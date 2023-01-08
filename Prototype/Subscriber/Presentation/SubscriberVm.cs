using Catel.IoC;
using Prototype.Subscriber.Contract;
using Prototype.Subscriber.Core;
using System;

namespace Prototype.Subscriber.Presentation
{
    internal class SubscriberVm
    {
        private readonly SubscriberM _model;
        private readonly ICommunicationService _communicationService;

        internal SubscriberVm()
        {
            _model = new SubscriberM();
            _communicationService = ServiceLocator.Default.ResolveType<ICommunicationService>();
        }

        internal void Start()
        {
            AskForPuplisherPort();
            AskForScenario();
        }

        #region Publisher

        private void AskForPuplisherPort()
        {
            int? portNumber = null;
            while (portNumber == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the port of the publisher port number:");

                portNumber = ReadPortNumber();
            }

            _model.PublisherPort = portNumber.Value;
        }

        private int? ReadPortNumber()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if (isNumber && portNumber <= 65535)
            {
                if (_communicationService.Test(_model.PublisherIpAdress, portNumber))
                    return portNumber;
            }

            return null;
        }

        #endregion


        private void AskForScenario()
        {
            int? scenario = null;
            while (scenario == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the scenario number:");

                scenario = ReadScenarioNumber();
            }

            _model.PublisherPort = scenario.Value;
        }

        private int? ReadScenarioSelection()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if (isNumber && portNumber <= 65535)
            {
                if (_communicationService.Test(_model.PublisherIpAdress, portNumber))
                    return portNumber;
            }

            return null;
        }


    }
}
