using System.IO;

namespace MDXReForged.Structs
{
    public class CSegment
    {
        public CVector3 Color;
        public float Alpha;
        public float Scaling;

        public CSegment()
        {
        }

        public CSegment(BinaryReader br)
        {
            Color = new CVector3(br);
            Alpha = br.ReadSingle();
            Scaling = br.ReadSingle();
        }
    }
}