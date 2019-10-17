using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class GEOA : BaseChunk, IReadOnlyList<GeosetAnimation>
    {
        private readonly List<GeosetAnimation> GeosetAnimations = new List<GeosetAnimation>();

        public GEOA(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
            {
                GeosetAnimations.Add(new GeosetAnimation(br));
            }
        }

        public GeosetAnimation this[int index] => GeosetAnimations[index];

        public int Count => GeosetAnimations.Count;

        public IEnumerator<GeosetAnimation> GetEnumerator() => GeosetAnimations.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GeosetAnimations.AsEnumerable().GetEnumerator();
    }

    public class GeosetAnimation
    {
        public int TotalSize;
        public float Alpha;
        public bool HasColorKeys;
        public CVector3 Color;
        public int GeosetId;

        public Track<float> AlphaKeys;
        public Track<CVector3> ColorKeys;

        public GeosetAnimation(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadInt32());

            Alpha = br.ReadSingle();
            HasColorKeys = br.ReadInt32() == 1;
            GeosetId = br.ReadInt32();
            Color = new CVector3(br);

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KGAO": AlphaKeys = new Track<float>(br); break;
                    case "KGAC": ColorKeys = new Track<CVector3>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}