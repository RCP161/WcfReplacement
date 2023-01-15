using System;
using System.Diagnostics;

namespace Prototype.Logging.BL
{
    internal class Logger : Contract.ILog
    {
        private bool _breakOnError = true;

        public void Log(Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("=============================================");
            Console.WriteLine();
            Console.WriteLine(exception.Message);
            Console.WriteLine();
            Console.WriteLine("=============================================");
            Console.WriteLine();

            if(_breakOnError)
                Debugger.Break();
        }
    }
}
