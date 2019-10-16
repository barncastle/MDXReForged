using System.IO;

namespace MDXReForged.Structs
{
    public class CCylinder
    {
        public CVector3 Base;
        public float Height;
        public float Radius;

        public CCylinder(BinaryReader br)
        {
            Base = new CVector3(br);
            Height = br.ReadSingle();
            Radius = br.ReadSingle();
        }
    }
}