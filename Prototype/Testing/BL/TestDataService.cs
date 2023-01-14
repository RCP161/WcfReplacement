using Prototype.Testing.Contract;
using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Testing.BL
{
    internal class TestDataService : ITestDataService
    {
        private readonly TestObjSerialiser _testObjSerialiser;

        public TestDataService()
        {
            _testObjSerialiser = new TestObjSerialiser();
        }

        public byte[] CreateArray(int size)
        {
            var array = new byte[size];
            Array.Clear(array, 0, array.Length);
            return array;
        }

        public bool IsTestArrayCorrect(byte[] data)
        {
            throw new NotImplementedException();
        }

        public SerialisationTestObj CreateSerialisationTestObj(int deep, int dataSize)
        {
            SerialisationTestObj obj = null;

            for(int i = 1; i <= deep; i++)
            {
                obj = new SerialisationTestObj()
                {
                    Name = $"TestObj {i,3}",
                    Number = i,
                    Data = CreateArray(dataSize),
                    SerialisationTestObjs = new List<SerialisationTestObj>()
                    {
                        CreateSerialisationTestObj(deep - 1, dataSize)
                    }
                };
            }

            return obj;
        }
        public bool IsCreateSerialisationTestObjCorrect(SerialisationTestObj obj, int deep, int dataSize)
        {
            return IsCreateSerialisationTestObjCorrect(obj, deep, dataSize, 1);
        }

        public byte[] CreateBinarySerialisationTestObj(int deep, int dataSize)
        {
            var testObj = CreateSerialisationTestObj(deep, dataSize);
            var xml = _testObjSerialiser.SerializeToXml(testObj);

            return Encoding.UTF8.GetBytes(xml);
        }

        public bool IsCreateSerialisationTestObjCorrect(byte[] data, int deep, int dataSize)
        {
            var xml = Encoding.UTF8.GetString(data);
            var testObj = _testObjSerialiser.DeserializeToXml(xml);

            return IsCreateSerialisationTestObjCorrect(testObj, deep, dataSize);
        }

        private bool IsCreateSerialisationTestObjCorrect(SerialisationTestObj obj, int deep, int dataSize, int number)
        {
            if(deep == 0 && obj == null)
                return true;
            if(deep != 0 && obj == null ||
                deep == 0 && obj != null)
                return false;

            bool isChildValid;
            foreach(var child in obj.SerialisationTestObjs)
            {
                isChildValid = IsCreateSerialisationTestObjCorrect(child, deep - 1, dataSize, number + 1);

                if(!isChildValid)
                    return false;
            }

            return obj.Name.Contains("TestObj ") &&
                obj.Name.Contains(number.ToString()) &&
                obj.Number == number &&
                obj.Data.Length == obj.DataSize;
        }
    }
}
