using System.IO;

namespace MDXReForged.Structs
{
    public class C34Matrix
    {
        public CVector3[] Columns;

        public C34Matrix() => Columns = new CVector3[4];

        public C34Matrix(BinaryReader br) : this()
        {
            Columns[0] = new CVector3(br);
            Columns[1] = new CVector3(br);
            Columns[2] = new CVector3(br);
            Columns[3] = new CVector3(br);
        }
    }
}