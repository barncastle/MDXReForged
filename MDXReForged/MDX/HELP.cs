using System.IO;

namespace MDXReForged.MDX
{
    public class HELP : EnumerableBaseChunk<Helper>
    {
        public HELP(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Helper(br));
        }
    }

    public class Helper : GenObject
    {
        public Helper(BinaryReader br)
        {
            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);
        }
    }
}