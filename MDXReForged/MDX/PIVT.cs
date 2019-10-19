using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class PIVT : EnumerableBaseChunk<CVector3>
    {
        public PIVT(BinaryReader br, uint version) : base(br, version)
        {
            for (int i = 0; i < Size / 0xC; i++)
                Values.Add(new CVector3(br));
        }
    }
}