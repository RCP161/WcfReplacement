﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Contract
{
    internal interface IScenarioService
    {
        void EvaluatePresentStandard();
        void EvaluateRequestVsStreamPerformance();
        void EvaluateSerialisationPerformance();
    }
}