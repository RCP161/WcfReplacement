using Prototype.Testing.Core;

namespace Prototype.Testing.Contract
{
    internal interface ITestDataService
    {
        byte[] CreateArray(int size);
        bool IsTestArrayCorrect(byte[] data);

        SerialisationTestObj CreateSerialisationTestObj(int deep, int dataSize);
        bool IsSerialisationTestObjCorrect(SerialisationTestObj obj, int dataSize);

        byte[] CreateBinarySerialisationTestObj(int deep, int dataSize);
        bool IsSerialisationTestObjCorrect(byte[] data, int dataSize);
    }
}
