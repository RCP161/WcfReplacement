using Prototype.Publisher.Contract;
using Prototype.Publisher.Contract.Events;
using Prototype.Testing.Contract;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Prototype.Publisher.BL
{
    internal class ScenarioService : IScenarioService
    {
        private readonly ICommunicationService _communicationService;
        private readonly ITestDataService _testDataService;

        private int _presentStandardExecutionCounterValue = 10;
        private int _presentStandardExecutionCounter;
        private bool _presentStandardSuccessful;
        private double _presentStandardTotalResponseTime;
        private System.Timers.Timer _presentStandardTimer;

        private const int DataSize10Kb = 10240;
        private const int DataSize64Kb = 65536;
        private const int DataSize5Mb = 5242880;

        public ScenarioService(ICommunicationService communicationService, ITestDataService testDataService)
        {
            _communicationService = communicationService;
            _testDataService = testDataService;

            _presentStandardTimer = new System.Timers.Timer();
            _presentStandardTimer.Elapsed += SendPresentStandardData;
        }

        public void EvaluatePresentStandard()
        {
            if(_presentStandardExecutionCounter > 0)
                return;

            _presentStandardSuccessful = true;
            _presentStandardTotalResponseTime = 0;
            _presentStandardExecutionCounter = _presentStandardExecutionCounterValue;
            _presentStandardTimer.Start();

            while(_presentStandardExecutionCounter > 0)
                Thread.Sleep(500);
        }

        private void SendPresentStandardData(object sender, ElapsedEventArgs e)
        {
            if(_presentStandardExecutionCounter == 1)
                _presentStandardTimer.Stop();

            _presentStandardExecutionCounter--;

            var data = _testDataService.CreateArray(DataSize64Kb);

            var stopwatch = Stopwatch.StartNew();
            var result = _communicationService.SendPresentStandad(data, DataSize64Kb);
            stopwatch.Stop();

            if(!result)
                _presentStandardSuccessful = false;

            _presentStandardTotalResponseTime = _presentStandardTotalResponseTime + stopwatch.ElapsedMilliseconds;

            if(_presentStandardExecutionCounter > 0)
                return;

            var args = new ScenarioFinishedEventArgs()
            {
                Text = "PresentStandard",
                Successful = _presentStandardSuccessful,
                AverageExecutionTime = _presentStandardTotalResponseTime / _presentStandardExecutionCounterValue / 1000
            };

            ScenarioFinishedEvent?.Invoke(this, args);
        }

        public void EvaluateRequestPerformance()
        {
            int dataSize = DataSize5Mb;
            int executionCounterValue = 50;
            int executionCounter = executionCounterValue;
            bool successful = true;
            double totalResponseTime = 0;

            while(executionCounter > 0)
            {
                executionCounter--;

                var stopwatch = Stopwatch.StartNew();

                var data = _testDataService.CreateArray(dataSize);
                var result = _communicationService.SendRequestPerformance(data, dataSize);

                stopwatch.Stop();

                if(!result)
                    successful = false;

                totalResponseTime = totalResponseTime + stopwatch.ElapsedMilliseconds;
            }

            var args = new ScenarioFinishedEventArgs()
            {
                Text = "EvaluateRequestPerformance",
                Successful = successful,
                AverageExecutionTime = totalResponseTime / executionCounterValue / 1000
            };

            ScenarioFinishedEvent?.Invoke(this, args);
        }

        public void EvaluateSerialisationPerformance()
        {
            int dataSize = DataSize10Kb;
            int deep = 100;
            int executionCounterValue = 50;
            int executionCounter = executionCounterValue;

            bool successfulProto = true;
            double totalResponseTimeProto = 0;

            while(executionCounter > 0)
            {
                executionCounter--;

                var stopwatch = Stopwatch.StartNew();

                var data = _testDataService.CreateSerialisationTestObj(deep, dataSize);
                var result = _communicationService.SendSerialisationProtoPerformance(data, dataSize);

                stopwatch.Stop();

                if(!result)
                    successfulProto = false;

                totalResponseTimeProto = totalResponseTimeProto + stopwatch.ElapsedMilliseconds;
            }


            bool successfulBinary = true;
            double totalResponseTimeBinary = 0;

            executionCounter = executionCounterValue;
            while(executionCounter > 0)
            {
                executionCounter--;

                var stopwatch = Stopwatch.StartNew();

                var data = _testDataService.CreateBinarySerialisationTestObj(deep, dataSize);
                var result = _communicationService.SendSerialisationBinaryPerformance(data, dataSize);

                stopwatch.Stop();

                if(!result)
                    successfulBinary = false;

                totalResponseTimeBinary = totalResponseTimeBinary + stopwatch.ElapsedMilliseconds;
            }


            var argsProto = new ScenarioFinishedEventArgs()
            {
                Text = "SerialisationPerformance-Proto",
                Successful = successfulProto,
                AverageExecutionTime = totalResponseTimeProto / executionCounterValue / 1000
            };

            var argsBinary = new ScenarioFinishedEventArgs()
            {
                Text = "SerialisationPerformance-Binary",
                Successful = successfulBinary,
                AverageExecutionTime = totalResponseTimeBinary / executionCounterValue / 1000
            };

            ScenarioFinishedEvent?.Invoke(this, argsProto);
            ScenarioFinishedEvent?.Invoke(this, argsBinary);
        }

        public event ScenarioFinishedEventHandler ScenarioFinishedEvent;
    }
}
