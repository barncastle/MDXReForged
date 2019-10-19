using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class LITE : EnumerableBaseChunk<Light>
    {
        public LITE(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Light(br));
        }
    }

    public class Light : GenObject
    {
        public uint TotalSize;
        public LIGHT_TYPE Type;

        public float AttenuationStart;
        public float AttenuationEnd;
        public CVector3 Color;
        public float Intensity;
        public CVector3 AmbientColor;
        public float AmbientIntensity;

        public Track<float> AttenStartKeys;
        public Track<float> AttenEndKeys;
        public Track<CVector3> ColorKeys;
        public Track<float> IntensityKeys;
        public Track<CVector3> AmbColorKeys;
        public Track<float> AmbIntensityKeys;
        public Track<float> VisibilityKeys;

        public Light(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            Type = (LIGHT_TYPE)br.ReadInt32();
            AttenuationStart = br.ReadSingle();
            AttenuationEnd = br.ReadSingle();
            Color = new CVector3(br);
            Intensity = br.ReadSingle();
            AmbientColor = new CVector3(br);    // added at version 700
            AmbientIntensity = br.ReadSingle(); // added at version 700

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KLAI": IntensityKeys = new Track<float>(br); break;
                    case "KLBI": AmbIntensityKeys = new Track<float>(br); break;
                    case "KLAV": VisibilityKeys = new Track<float>(br); break;
                    case "KLAC": ColorKeys = new Track<CVector3>(br); break;
                    case "KLBC": AmbColorKeys = new Track<CVector3>(br); break;
                    case "KLAS": AttenStartKeys = new Track<float>(br); break;
                    case "KLAE": AttenEndKeys = new Track<float>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}