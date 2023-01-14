using Catel.IoC;
using Prototype.Publisher.Presentation;
using Prototype.Subscriber.Presentation;
using System;
using System.Net;
using System.Net.Sockets;

namespace Prototype
{
    internal class Program
    {
        private static AppType? _appType;
        private static int? _portNumber;

        static void Main(string[] args)
        {
            Welcome();
            AskAppType();
            AskPortNumber();
            RegisterServices();

            var config = new ServerConfig()
            {
                IpAdress = Constants.LocalHost,
                PortNumber = _portNumber.Value
            };

            switch(_appType)
            {
                case AppType.Publisher:
                    PublisherVm p = new PublisherVm(config);
                    p.Start();
                    break;
                case AppType.Subscriber:
                    SubscriberVm s = new SubscriberVm(config);
                    s.Start();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region Init

        private static void Welcome()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to the WCF replacement prototype");
            Console.WriteLine("========================================");
        }

        private static void AskAppType()
        {
            while(_appType == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please select type:");
                Console.WriteLine("1 - TBM");
                Console.WriteLine("2 - Office");
                Console.WriteLine();

                ReadAppType();
            }
        }

        private static void ReadAppType()
        {
            var input = Console.ReadLine();
            var isValid = Enum.TryParse<AppType>(input, out AppType appType);

            if(isValid)
                _appType = appType;
        }

        private static void AskPortNumber()
        {
            while(_portNumber == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the port:");
                Console.WriteLine();

                ReadPortNumber();
            }
        }

        private static void ReadPortNumber()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if(isNumber && portNumber <= 65535)
            {
                if(CheckPortIsFree(portNumber))
                    _portNumber = portNumber;
            }
        }

        private static bool CheckPortIsFree(int portNumber)
        {
            IPAddress ipAddress = Dns.GetHostEntry(Constants.LocalHost).AddressList[0];
            try
            {
                TcpListener tcpListener = new TcpListener(ipAddress, portNumber);
                tcpListener.Start();
            }
            catch(SocketException ex)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Services

        private static void RegisterServices()
        {
            // Logger?
            ServiceLocator.Default.RegisterType<Testing.Contract.ITestDataService, Testing.BL.TestDataService>();

            switch(_appType)
            {
                case AppType.Publisher:
                    RegisterPublisherServices();
                    break;
                case AppType.Subscriber:
                    RegisterSubscriberServices();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void RegisterPublisherServices()
        {
            ServiceLocator.Default.RegisterType<Publisher.Contract.ICommunicationService, Publisher.BL.CommunicationService>();
            ServiceLocator.Default.RegisterType<Publisher.Contract.IScenarioService, Publisher.BL.ScenarioService>();
        }

        private static void RegisterSubscriberServices()
        {
            ServiceLocator.Default.RegisterType<Subscriber.Contract.ICommunicationService, Subscriber.BL.CommunicationService>();
        }

        #endregion
    }
}