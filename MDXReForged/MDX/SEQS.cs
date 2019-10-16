using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class SEQS : BaseChunk, IReadOnlyCollection<Sequence>
    {
        private Sequence[] Sequences;

        public SEQS(BinaryReader br, uint version) : base(br)
        {
            Sequences = new Sequence[Size / 132];
            for (int i = 0; i < Sequences.Length; i++)
                Sequences[i] = new Sequence(br);
        }

        public int Count => Sequences.Length;

        public IEnumerator<Sequence> GetEnumerator() => Sequences.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Sequences.AsEnumerable().GetEnumerator();
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