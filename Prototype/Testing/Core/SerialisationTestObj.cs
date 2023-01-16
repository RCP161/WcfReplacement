using System.Collections.Generic;

namespace Prototype.Testing.Core
{
    public class SerialisationTestObj
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public byte[] Data { get; set; }
        public List<SerialisationTestObj> SerialisationTestObjs { get; set; }
    }
}
