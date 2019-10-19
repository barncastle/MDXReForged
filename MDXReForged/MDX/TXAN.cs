using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class TXAN : EnumerableBaseChunk<TextureAnimation>
    {
        public TXAN(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new TextureAnimation(br));
        }
    }

    public class TextureAnimation
    {
        public uint TotalSize;

        public Track<CVector3> TranslationKeys;
        public Track<CVector4> RotationKeys;
        public Track<CVector3> ScaleKeys;

        public TextureAnimation(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KTAT": TranslationKeys = new Track<CVector3>(br); break;
                    case "KTAR": RotationKeys = new Track<CVector4>(br); break;
                    case "KTAS": ScaleKeys = new Track<CVector3>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}