using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class SEQS : EnumerableBaseChunk<Sequence>
    {
        public SEQS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Sequence(br));
        }
    }

    public class Sequence
    {
        public string Name;
        public int MinTime;
        public int MaxTime;
        public float MoveSpeed;
        public bool NonLooping;
        public float Frequency;
        public uint SyncPoint;
        public CExtent Bounds;

        public Sequence(BinaryReader br)
        {
            Name = br.ReadCString(Constants.SizeName);
            MinTime = br.ReadInt32();
            MaxTime = br.ReadInt32();
            MoveSpeed = br.ReadSingle();

            NonLooping = br.ReadInt32() == 1;
            Frequency = br.ReadSingle();
            SyncPoint = br.ReadUInt32();
            Bounds = new CExtent(br);
        }
    }
}