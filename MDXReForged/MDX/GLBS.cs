using System.IO;

namespace MDXReForged.MDX
{
    public class GLBS : EnumerableBaseChunk<int>
    {
        public GLBS(BinaryReader br, uint version) : base(br, version)
        {
            for (int i = 0; i < Size / 4; i++)
                Values.Add(br.ReadInt32());
        }
    }
}