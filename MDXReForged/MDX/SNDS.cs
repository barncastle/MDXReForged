using System.IO;

namespace MDXReForged.MDX
{
    public class SNDS : EnumerableBaseChunk<SoundTrack>
    {
        public SNDS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new SoundTrack(br));
        }
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