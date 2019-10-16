using System.IO;

namespace MDXReForged.Structs
{
    public class CBox
    {
        public CVector3 Min;
        public CVector3 Max;

        public CBox()
        {
            Min = new CVector3();
            Max = new CVector3();
        }

        public CBox(BinaryReader br)
        {
            Min = new CVector3(br);
            Max = new CVector3(br);
        }
    }
}