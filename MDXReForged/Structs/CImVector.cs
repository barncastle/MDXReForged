using System.IO;

namespace MDXReForged.Structs
{
    public class CImVector
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;

        public CImVector()
        {
        }

        public CImVector(BinaryReader br)
        {
            B = br.ReadByte();
            G = br.ReadByte();
            R = br.ReadByte();
            A = br.ReadByte();
        }
    }
}