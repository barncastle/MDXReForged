using System.IO;

namespace MDXReForged.MDX
{
    public class EVTS : EnumerableBaseChunk<Event>
    {
        public EVTS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Event(br));
        }
    }

    public class Event : GenObject
    {
        public uint TotalSize;
        public SimpleTrack EventKeys;

        public Event(BinaryReader br)
        {
            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            if (br.HasTag("KEVT"))
                EventKeys = new SimpleTrack(br, false);
        }
    }
}