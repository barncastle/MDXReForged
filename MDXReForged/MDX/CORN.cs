using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class CORN : BaseChunk, IReadOnlyList<ParticleEmitterPopcorn>
    {
        private readonly List<ParticleEmitterPopcorn> PopcornEmitters = new List<ParticleEmitterPopcorn>();

        public CORN(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                PopcornEmitters.Add(new ParticleEmitterPopcorn(br));
        }

        public ParticleEmitterPopcorn this[int index] => PopcornEmitters[index];

        public int Count => PopcornEmitters.Count;

        public IEnumerator<ParticleEmitterPopcorn> GetEnumerator() => PopcornEmitters.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => PopcornEmitters.AsEnumerable().GetEnumerator();
    }

    public class ParticleEmitterPopcorn : GenObject
    {
        public uint TotalSize;
        public CExtent Extent;
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

            Extent = new CExtent(br);
            UInt1 = br.ReadUInt32();
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