using System.IO;

namespace MDXReForged.MDX
{
    public class VERS : BaseChunk
    {
        public new uint Version;

        public VERS(BinaryReader br, uint version) : base(br, version) => Version = br.ReadUInt32();
    }
}