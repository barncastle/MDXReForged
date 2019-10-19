using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class CORN : EnumerableBaseChunk<ParticleEmitterPopcorn>
    {
        public CORN(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new ParticleEmitterPopcorn(br));
        }
    }

    public class ParticleEmitterPopcorn : GenObject
    {
        public uint TotalSize;
        public float Float1;
        public float Float2;
        public float Float3;
        public float Float4;
        public float Float5;
        public float Float6;
        public float Float7;
        public uint UInt1;
        public string FilePath;
        public string AnimVisibilityGuide;

        public Track<float> SpeedKeys;
        public Track<float> VisibilityKeys;
        public Track<float> LifespanKeys;
        public Track<float> AlphaKeys;
        public Track<float> EmissionRateKeys;
        public Track<CVector3> ColorKeys;

        public ParticleEmitterPopcorn(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            Float1 = br.ReadSingle();
            Float2 = br.ReadSingle();
            Float3 = br.ReadSingle();
            Float4 = br.ReadSingle();
            Float5 = br.ReadSingle();
            Float6 = br.ReadSingle();
            Float7 = br.ReadSingle();
            UInt1 = br.ReadUInt32(); // boolean?
            FilePath = br.ReadCString(Constants.SizeFileName);
            AnimVisibilityGuide = br.ReadCString(Constants.SizeFileName);

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadCString(4);
                switch (tagname)
                {
                    case "KPPS": SpeedKeys = new Track<float>(br); break;
                    case "KPPV": VisibilityKeys = new Track<float>(br); break;
                    case "KPPL": LifespanKeys = new Track<float>(br); break;
                    case "KPPA": AlphaKeys = new Track<float>(br); break;
                    case "KPPE": EmissionRateKeys = new Track<float>(br); break;
                    case "KPPC": ColorKeys = new Track<CVector3>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}