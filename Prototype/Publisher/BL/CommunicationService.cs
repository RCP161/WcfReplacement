using Grpc.Core;
using Prototype.Publisher.Contract;
using Prototype.Publisher.Contract.Events;
using Prototype.Subscriber.BL;
using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class CommunicationService : ICommunicationService, IDisposable
    {
        private Server _server;
        private PublisherService _publisherService;
        private readonly Dictionary<ServerConfig, Channel> _subscribers;

        public CommunicationService()
        {
            _subscribers = new Dictionary<ServerConfig, Channel>();
        }

        public void StartServiceHost(IServerConfig localServerConfig)
        {
            _publisherService = new PublisherService(_subscribers, localServerConfig);
            _publisherService.SubscriberEvent += PublisherService_SubscriberEvent;

            _server = new Server()
            {
                Services = { PublisherGrpcService.BindService(_publisherService) },
                Ports = { new ServerPort(localServerConfig.IpAdress, localServerConfig.PortNumber, ServerCredentials.Insecure) }
            };

            _server.Start();
        }

        public async Task StopServiceHostAsync()
        {
            foreach(var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    client.Unsubscribed(new Google.Protobuf.WellKnownTypes.Empty());
                }
                catch
                {
                    // Log
                }
            }

            await _server.ShutdownAsync();
        }

        public bool SendPresentStandad(byte[] data, int size)
        {
            bool successful = true;

            foreach(var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    TestByteArray model = new TestByteArray()
                    {
                        Data = Google.Protobuf.ByteString.CopyFrom(data),
                        DataSize = size
                    };

                    var response = client.PresentStandard(model);

                    if(!response.Successful)
                        successful = false;
                }
                catch
                {
                    // Log
                    successful = false;
                }
            }

            return successful;
        }

        public bool SendRequestPerformance(byte[] data, int size)
        {
            bool successful = true;

            foreach(var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    TestByteArray model = new TestByteArray()
                    {
                        Data = Google.Protobuf.ByteString.CopyFrom(data),
                        DataSize = size
                    };

                    var response = client.RequestPerformance(model);

                    if(!response.Successful)
                        successful = false;
                }
                catch
                {
                    // Log
                    successful = false;
                }
            }

            return successful;
        }

        public bool SendSerialisationBinaryPerformance(byte[] data, int size, int deep)
        {
            bool successful = true;

            foreach(var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    SerialisationBinaryModel model = new SerialisationBinaryModel()
                    {
                        Data = Google.Protobuf.ByteString.CopyFrom(data),
                        DataSize = size,
                        Deep = deep
                    };

                    var response = client.SerialisationBinaryPerformance(model);

                    if(!response.Successful)
                        successful = false;
                }
                catch
                {
                    // Log
                    successful = false;
                }
            }

            return successful;
        }

        public bool SendSerialisationProtoPerformance(SerialisationTestObj serialisationTestObj, int size, int deep)
        {
            bool successful = true;

            foreach(var s in _subscribers)
            {
                var client = new SubscriberGrpcService.SubscriberGrpcServiceClient(s.Value);

                try
                {
                    var model = GetSerialisationProtoModel(serialisationTestObj, size, deep);
                    var response = client.SerialisationProtoPerformance(model);

                    if(!response.Successful)
                        successful = false;
                }
                catch
                {
                    // Log
                    successful = false;
                }
            }

            return successful;
        }

        private SerialisationProtoModel GetSerialisationProtoModel(SerialisationTestObj serialisationTestObj, int size, int deep)
        {
            var childs = new Google.Protobuf.Collections.RepeatedField<SerialisationProtoModel>();

            foreach(var child in serialisationTestObj.SerialisationTestObjs)
            {
                childs.Add(GetSerialisationProtoModel(child, size, deep));
            }

            SerialisationProtoModel model = new SerialisationProtoModel()
            {
                Name = serialisationTestObj.Name,
                Number = serialisationTestObj.Number,
                DataSize = size,
                Deep = deep,
                Data = Google.Protobuf.ByteString.CopyFrom(serialisationTestObj.Data),
            };

            model.SerialsiationTestObjs.Add(childs);

            return model;
        }


        public event SubscriberEventHandler SubscriberEvent;

        private void PublisherService_SubscriberEvent(object sender, Contract.Events.SubscriberEventArgs e)
        {
            SubscriberEvent?.Invoke(sender, e);
        }

        public void Dispose()
        {
            if(_publisherService != null)
                _publisherService.SubscriberEvent -= PublisherService_SubscriberEvent;
        }
    }
}
