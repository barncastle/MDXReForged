using System.IO;

namespace MDXReForged.Structs
{
    public class CVector2
    {
        public float X;
        public float Y;

        public CVector2()
        {
        }

        public CVector2(BinaryReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
        }

        public override string ToString() => $"X: {X}, Y: {Y}";
    }
}