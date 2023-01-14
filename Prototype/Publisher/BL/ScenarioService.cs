using Prototype.Publisher.Contract;
using Prototype.Testing.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Prototype.Publisher.BL
{
    internal class ScenarioService : IScenarioService
    {
        private readonly ICommunicationService _communicationService;
        private readonly ITestDataService _testDataService;

        private const int MessageSize10Kb = 10240;
        private const int MessageSize64Kb = 65536;
        private const int MessageSize5Mb = 5242880;


        public ScenarioService(ICommunicationService communicationService, ITestDataService testDataService)
        {
            _communicationService = communicationService;
            _testDataService = testDataService;
        }

        public void EvaluatePresentStandard()
        {
            new System.Threading.Timer(SendPresentStandard, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
        }

        public void EvaluateRequestPerformance()
        {
            throw new NotImplementedException();
        }

        public void EvaluateSerialisationPerformance()
        {
            throw new NotImplementedException();
        }

        private void SendData(TimeSpan period, int messageSize64Kb, int repetitions)
        {
        }

        private void SendPresentStandard(object state)
        {
            throw new NotImplementedException();

            _communicationService.SendPresentStandad();
        }
    }
}
