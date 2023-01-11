using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.BL
{
    internal class DataGeneration
    {
        private byte[] _64KbMessage;

        internal byte[] Create64KbMessage()
        {
            if (_64KbMessage == null)
            {
                _64KbMessage = new byte[1024 * 64];
                Array.Clear(_64KbMessage, 0, _64KbMessage.Length);
            }

            return _64KbMessage;
        }


        // gRPC UTF8
        // https://developers.google.com/protocol-buffers/docs/encoding

        // System.Text.UTF8Encoding.GetByteCount(string);


        // Serialisation Node - Node - Node - ...
        // => XML
        // => XML => Binär
        // => Proto?
    }
}
