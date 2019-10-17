using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class HELP : BaseChunk, IReadOnlyList<Helper>
    {
        private readonly List<Helper> Helpers = new List<Helper>();

        public HELP(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Helpers.Add(new Helper(br));
        }

        public Helper this[int index] => Helpers[index];

        public int Count => Helpers.Count;

        public IEnumerator<Helper> GetEnumerator() => Helpers.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Helpers.AsEnumerable().GetEnumerator();
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