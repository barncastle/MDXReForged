using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class BONE : BaseChunk, IReadOnlyCollection<Bone>
    {
        private List<Bone> Bones = new List<Bone>();

        public BONE(BinaryReader br, uint version) : base(br)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Bones.Add(new Bone(br));
        }

        public int Count => Bones.Count;

        public IEnumerator<Bone> GetEnumerator() => Bones.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Bones.AsEnumerable().GetEnumerator();
    }

    public class Bone : GenObject
    {
        public int GeosetId;
        public int GeosetAnimId;

        public Bone(BinaryReader br)
        {
            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            GeosetId = br.ReadInt32();
            GeosetAnimId = br.ReadInt32();
        }
    }
}