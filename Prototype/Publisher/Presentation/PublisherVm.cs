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
        private readonly IScenarioService _scenarioService;

        public PublisherVm(ServerConfig config)
        {
            _localServerConfig = config;
            _communicationService = ServiceLocator.Default.ResolveType<ICommunicationService>();
            _scenarioService = ServiceLocator.Default.ResolveType<IScenarioService>();

            _scenarioService.ScenarioFinishedEvent += ScenarioService_ScenarioFinishedEvent;
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
            string action = "subscribed";
            if(!e.Subscribed)
                action = "unsubscribed";

            Console.WriteLine($"{Constants.InfoPrefix}{e.ServerConfig} {action}");
        }

        #endregion

        #region Scenario

        private void AskForScenario()
        {
            Scenario? scenario = null;
            while(scenario != Scenario.Stop)
            {
                WriteScenarioOptions();
                scenario = ReadScenarioNumber();

                if(scenario == null)
                    continue;

                ExecuteScenario(scenario.Value);
            }
        }

        private void WriteScenarioOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter the scenario number:");

            foreach(var sc in Enum.GetValues(typeof(Scenario)))
            {
                Console.WriteLine($"{(int)sc} - {sc}");
            }
            Console.WriteLine();
        }

        private Scenario? ReadScenarioNumber()
        {
            var portText = Console.ReadLine();
            var isScenario = Enum.TryParse<Scenario>(portText, out Scenario scenario);

            Console.WriteLine();

            if(!isScenario)
                return null;

            return scenario;
        }

        private void ExecuteScenario(Scenario value)
        {
            switch(value)
            {
                case Scenario.Stop:
                    _communicationService.StopServiceHostAsync().GetAwaiter().GetResult();
                    break;
                case Scenario.PresentStandard:
                    _scenarioService.EvaluatePresentStandard();
                    break;
                case Scenario.RequestPerformance:
                    _scenarioService.EvaluateRequestPerformance();
                    break;
                case Scenario.SerialisationPerformance:
                    _scenarioService.EvaluateSerialisationPerformance();
                    break;
                default:
                    return;
            }
        }

        private void ScenarioService_ScenarioFinishedEvent(object sender, Contract.Events.ScenarioFinishedEventArgs e)
        {
            string successful = "successfully";
            if(!e.Successful)
                successful = "not successful";

            Console.WriteLine($"{Constants.InfoPrefix}Scenario {e.Text} finished {successful}. Average execution time: {e.AverageExecutionTime} sec.");
        }

        #endregion

    }
}
