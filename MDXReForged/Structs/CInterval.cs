using System.IO;

namespace MDXReForged.Structs
{
    public class CInterval
    {
        public int Start;
        public int End;
        public int Repeat;

        public CInterval()
        {
        }

        public CInterval(BinaryReader br)
        {
            Start = br.ReadInt32();
            End = br.ReadInt32();
            Repeat = br.ReadInt32();
        }
    }
}