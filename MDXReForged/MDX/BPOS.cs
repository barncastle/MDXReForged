using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    /// <summary>
    /// BindPose
    /// </summary>
    public class BPOS : EnumerableBaseChunk<C34Matrix>
    {
        public BPOS(BinaryReader br, uint version) : base(br, version)
        {
            int count = br.ReadInt32();
            for (int i = 0; i < count; i++)
                Values.Add(new C34Matrix(br));
        }
    }
}