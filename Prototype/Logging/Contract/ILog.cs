using System;

namespace Prototype.Logging.Contract
{
    internal interface ILog
    {
        void Log(Exception exception);
    }
}
