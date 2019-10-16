using System.IO;

namespace MDXReForged.Structs
{
    public class CVector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public CVector4()
        {
        }

        public CVector4(BinaryReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            W = br.ReadSingle();
        }

        public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}, W: {W}";
    }
}