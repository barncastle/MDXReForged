using System.IO;

namespace MDXReForged.MDX
{
    public class BONE : EnumerableBaseChunk<Bone>
    {
        public BONE(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Bone(br));
        }
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