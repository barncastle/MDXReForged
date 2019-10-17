using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class ATCH : BaseChunk, IReadOnlyList<Attachment>
    {
        private readonly List<Attachment> Attachments = new List<Attachment>();

        public ATCH(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Attachments.Add(new Attachment(br));
        }

        public Attachment this[int index] => Attachments[index];

        public int Count => Attachments.Count;

        public IEnumerator<Attachment> GetEnumerator() => Attachments.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Attachments.AsEnumerable().GetEnumerator();
    }

    public class Attachment : GenObject
    {
        public uint TotalSize;
        public int AttachmentId;
        public byte Padding;
        public string Path;
        public Track<float> VisibilityKeys;

        public Attachment(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            Path = br.ReadCString(Constants.SizeFileName);
            AttachmentId = br.ReadInt32();

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KATV": VisibilityKeys = new Track<float>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}