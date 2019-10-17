using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class MODL : BaseChunk
    {
        public string Name;
        public string AnimationFile;
        public CExtent Bounds;
        public uint BlendTime;

        public MODL(BinaryReader br, uint version) : base(br, version)
        {
            Name = br.ReadCString(Constants.SizeName);
            AnimationFile = br.ReadCString(Constants.SizeFileName);
            Bounds = new CExtent(br);
            BlendTime = br.ReadUInt32();
        }
    }
}