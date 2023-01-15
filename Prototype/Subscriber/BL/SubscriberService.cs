using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Prototype.Logging.Contract;
using Prototype.Publisher.Contract.Events;
using Prototype.Subscriber.Contract.Events;
using Prototype.Testing.Contract;
using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Subscriber.BL
{
    internal class SubscriberService : SubscriberGrpcService.SubscriberGrpcServiceBase
    {
        private readonly ITestDataService _testDataService;
        private readonly ILog _log;

        public SubscriberService(ITestDataService testDataService, ILog log)
        {
            _testDataService = testDataService;
            _log = log;
        }

        public override Task<ResponseMessage> PresentStandard(TestByteArray request, ServerCallContext context)
        {
            RaiseSubscriberEvent("PresentStandard");

            bool successful = TryGetData(request.Data, request.DataSize, out byte[] data);

            if(!successful)
                return GetFinishResponse(successful);

            successful = _testDataService.IsTestArrayCorrect(data);
            return GetFinishResponse(successful);
        }

        public override Task<ResponseMessage> RequestPerformance(TestByteArray request, ServerCallContext context)
        {
            RaiseSubscriberEvent("RequestPerformance");

            bool successful = TryGetData(request.Data, request.DataSize, out byte[] data);

            if(!successful)
                return GetFinishResponse(successful);

            successful = _testDataService.IsTestArrayCorrect(data);
            return GetFinishResponse(successful);
        }

        public override Task<ResponseMessage> SerialisationBinaryPerformance(SerialisationBinaryModel request, ServerCallContext context)
        {
            RaiseSubscriberEvent("SerialisationPerformance - Binary");

            bool successful = TryGetData(request.Data, request.DataSize, out byte[] data);

            if(!successful)
                return GetFinishResponse(successful);

            successful = _testDataService.IsCreateSerialisationTestObjCorrect(data, request.Deep, request.DataSize);
            return GetFinishResponse(successful);
        }

        public override Task<ResponseMessage> SerialisationProtoPerformance(SerialisationProtoModel request, ServerCallContext context)
        {
            RaiseSubscriberEvent("SerialisationPerformance - Proto");

            var serialisationTestObj = GetSerialisationTestObj(request);
            bool successful = _testDataService.IsCreateSerialisationTestObjCorrect(serialisationTestObj, request.Deep, request.DataSize);
            return GetFinishResponse(successful);
        }

        public override Task<ResponseMessage> Unsubscribed(Empty request, ServerCallContext context)
        {
            RaiseSubscriberEvent("Publisher ends subscribtion");
            // Handle if necessary
            return GetFinishResponse(true);
        }

        private Task<ResponseMessage> GetFinishResponse(bool success)
        {
            var message = new ResponseMessage()
            {
                Message = "finished",
                Successful = success
            };

            return Task.FromResult(message);
        }

        private bool TryGetData(ByteString bytes, int size, out byte[] data)
        {
            data = new byte[size];

            try
            {
                bytes.CopyTo(data, 0);

            }
            catch(Exception ex)
            {
                _log.Log(ex);
                return false;
            }

            return true;
        }

        private SerialisationTestObj GetSerialisationTestObj(SerialisationProtoModel model)
        {
            if(model.Deep == 0 &&
                model.Number == 0 &&
                model.Name.Length == 0 &&
                model.DataSize == 0 &&
                model.Data.Length == 0)
                return null;

            SerialisationTestObj obj = new SerialisationTestObj()
            {
                Name = model.Name,
                Number = model.Number,
                DataSize = model.DataSize,
                SerialisationTestObjs = new List<SerialisationTestObj>()
            };

            model.Data.CopyTo(obj.Data, 0);

            foreach(var child in model.SerialsiationTestObjs)
                obj.SerialisationTestObjs.Add(GetSerialisationTestObj(child));

            return obj;
        }

        public event DataRecievedEventHandler DataRecievedEvent;

        private void RaiseSubscriberEvent(string scenarioName)
        {
            var args = new DataRecievedEventArgs()
            {
                ScenarioName = scenarioName
            };

            DataRecievedEvent?.Invoke(this, args);
        }
    }
}