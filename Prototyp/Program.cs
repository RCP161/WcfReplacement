using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the WCF replacement prototype");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Please enter the port:");


            Console.ReadLine();

            ServerHost host = new ServerHost();

        }
    }
}
