using Prototype.Testing.Contract;
using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Testing.BL
{
    internal class TestDataService : ITestDataService
    {
        private byte[] _testArray;
        private byte[] _textArray;
        private const int StartNumber = 1;
        private readonly TestObjSerialiser _testObjSerialiser;

        public TestDataService()
        {
            _testObjSerialiser = new TestObjSerialiser();

            _testArray = new byte[0];
            _textArray = Encoding.Unicode.GetBytes(Constants.TestText);
        }

        public byte[] CreateArray(int size)
        {
            if(size > _testArray.Length)
                ExpandTestArray(size);

            var array = new byte[size];
            Array.Copy(_testArray, array, size);
            return array;
        }

        public bool IsTestArrayCorrect(byte[] data)
        {
            if(data.Length > _testArray.Length)
                ExpandTestArray(data.Length);

            var array = new byte[data.Length];
            Array.Copy(_testArray, array, data.Length);

            return data.SequenceEqual(array);
        }

        public SerialisationTestObj CreateSerialisationTestObj(int deep, int dataSize)
        {
            return CreateSerialisationTestObj(StartNumber, deep, dataSize);
        }

        private void ExpandTestArray(int size)
        {
            int iterations = (int)Math.Ceiling((double)size / _textArray.Length);
            _testArray = new byte[_textArray.Length * iterations];

            int index;

            for(int i = 0; i < iterations; i++)
            {
                index = _textArray.Length * i;
                Array.Copy(_textArray, 0, _testArray, index, _textArray.Length);
            }
        }

        private SerialisationTestObj CreateSerialisationTestObj(int number, int deep, int dataSize)
        {
            SerialisationTestObj obj = new SerialisationTestObj()
            {
                Name = $"TestObj {number,3}",
                Number = number,
                Data = CreateArray(dataSize),
                SerialisationTestObjs = new List<SerialisationTestObj>()
            };

            if(deep > 0)
                obj.SerialisationTestObjs.Add(CreateSerialisationTestObj(number + 1, deep - 1, dataSize));

            return obj;
        }

        public bool IsCreateSerialisationTestObjCorrect(SerialisationTestObj obj, int deep, int dataSize)
        {
            return IsCreateSerialisationTestObjCorrect(obj, deep, dataSize, StartNumber);
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
