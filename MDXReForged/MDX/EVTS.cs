using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class EVTS : BaseChunk, IReadOnlyCollection<Event>
    {
        private List<Event> Events = new List<Event>();

        public EVTS(BinaryReader br, uint version) : base(br)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Events.Add(new Event(br));
        }

        public int Count => Events.Count;

        public IEnumerator<Event> GetEnumerator() => Events.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Events.AsEnumerable().GetEnumerator();
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