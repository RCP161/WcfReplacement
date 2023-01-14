using System.Collections.Generic;

namespace Prototype.Testing.Core
{
    internal class SerialisationTestObj
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int DataSize { get; set; }
        public byte[] Data { get; set; }
        public List<SerialisationTestObj> SerialisationTestObjs { get; set; }
    }
}
