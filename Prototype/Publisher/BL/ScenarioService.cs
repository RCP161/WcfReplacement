using Prototype.Publisher.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class ScenarioService : IScenarioService
    {
        private readonly ICommunicationService _communicationService;

        public ScenarioService(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        public void EvaluatePresentStandard()
        {
            throw new NotImplementedException();
        }

        public void EvaluateRequestVsStreamPerformance()
        {
            throw new NotImplementedException();
        }

        public void EvaluateSerialisationPerformance()
        {
            throw new NotImplementedException();
        }
    }
}
