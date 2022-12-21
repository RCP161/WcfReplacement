using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC.Framework.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost();
            host.Start();

            Console.WriteLine("Server is listening ...");
            Console.WriteLine("Press any key to stop the server ...");
            Console.ReadKey();
        }
    }
}
