using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class SNDS : BaseChunk, IReadOnlyCollection<SoundTrack>
    {
        private SoundTrack[] Sounds;

        public SNDS(BinaryReader br, uint version) : base(br)
        {
            Sounds = new SoundTrack[Size / 272];
            for (int i = 0; i < Sounds.Length; i++)
                Sounds[i] = new SoundTrack(br);
        }

        public int Count => Sounds.Length;

        public IEnumerator<SoundTrack> GetEnumerator() => Sounds.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Sounds.AsEnumerable().GetEnumerator();
    }

    public class SoundTrack
    {
        public string Filename;
        public float Volume;
        public float Pitch;
        public uint Flags;

        public SoundTrack(BinaryReader br)
        {
            Filename = br.ReadCString(Constants.SizeFileName);
            Volume = br.ReadSingle();
            Pitch = br.ReadSingle();
            Flags = br.ReadUInt32();
        }
    }
}