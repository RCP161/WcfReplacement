using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Catel.IoC;
using Prototype.Publisher.Presentation;

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

            switch (_appType)
            {
                case AppType.Publisher:
                    PublisherVm vm = new PublisherVm();
                    vm.Start();
                    break;
                case AppType.Subscriber:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        #region Init

        private static void Welcome()
        {
            Console.WriteLine("Welcome to the WCF replacement prototype");
            Console.WriteLine("========================================");
        }

        private static void AskAppType()
        {
            while (_appType == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please select type:");
                Console.WriteLine("1 - TBM");
                Console.WriteLine("2 - Office");

                ReadAppType();
            }
        }

        private static void ReadAppType()
        {
            var input = Console.ReadLine();
            var isValid = Enum.TryParse<AppType>(input, out AppType appType);

            if (isValid)
                _appType = appType;
        }

        private static void AskPortNumber()
        {
            while (_portNumber == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the port:");

                ReadPortNumber();
            }
        }

        private static void ReadPortNumber()
        {
            var portText = Console.ReadLine();
            var isNumber = Int32.TryParse(portText, out int portNumber);

            if (isNumber && portNumber <= 65535)
            {
                if (CheckPortIsFree(portNumber))
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
            catch (SocketException ex)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Services

        private static void RegisterServices()
        {
            ServiceLocator.Default.RegisterInstance<IAppConfig>(new AppConfig(_appType.Value, _portNumber.Value));
        }

        #endregion
    }
}