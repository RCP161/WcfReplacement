using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Testing.Contract
{
    internal interface ITestDataService
    {
        byte[] CreateArray(int size);
        bool IsTestArrayCorrect(byte[] data);

        SerialisationTestObj CreateSerialisationTestObj(int deep, int dataSize);
        bool IsCreateSerialisationTestObjCorrect(SerialisationTestObj obj, int deep, int dataSize);

        byte[] CreateBinarySerialisationTestObj(int deep, int dataSize);
        bool IsCreateSerialisationTestObjCorrect(byte[] data, int deep, int dataSize);
    }
}
