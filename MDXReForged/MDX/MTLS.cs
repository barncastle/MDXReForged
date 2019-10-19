using System.Collections.Generic;
using System.IO;

namespace MDXReForged.MDX
{
    public class MTLS : EnumerableBaseChunk<Material>
    {
        public MTLS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new Material(br, version));
        }
    }

    public class Material
    {
        public uint TotalSize;
        public int PriorityPlane;
        public string Shader;
        public uint Flags;
        public uint NrOfLayers;
        public List<Layer> Layers = new List<Layer>();

        public Material(BinaryReader br, uint version)
        {
            TotalSize = br.ReadUInt32();
            PriorityPlane = br.ReadInt32();
            Flags = br.ReadUInt32();

            if (version >= 900)
                Shader = br.ReadCString(Constants.SizeName);

            br.AssertTag("LAYS");
            NrOfLayers = br.ReadUInt32();
            for (int i = 0; i < NrOfLayers; i++)
                Layers.Add(new Layer(br, version));
        }
    }

    public class Layer
    {
        public uint TotalSize;
        public MDLTEXOP BlendMode;
        public MDLGEO Flags;
        public int TextureAnimationId;
        public uint TextureId;
        public int CoordId;
        public float Alpha;
        public float EmissiveGain; // guesstimated MDL token

        public int PriorityPlane;

        public Track<int> FlipKeys;
        public Track<float> AlphaKeys;
        public Track<float> EmissiveGainKeys;

        public Layer(BinaryReader br, uint version)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());
            BlendMode = (MDLTEXOP)br.ReadInt32();
            Flags = (MDLGEO)br.ReadUInt32();
            TextureId = br.ReadUInt32();
            TextureAnimationId = br.ReadInt32();
            CoordId = br.ReadInt32();
            Alpha = br.ReadSingle();

            // this is new but the client doesn't actually check the version!
            // sub_7FF6830300A0
            if (br.BaseStream.Position < end && version >= 900)
                EmissiveGain = br.ReadSingle();

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadCString(4);
                switch (tagname)
                {
                    case "KMTA": AlphaKeys = new Track<float>(br); break;
                    case "KMTF": FlipKeys = new Track<int>(br); break;
                    case "KMTE": EmissiveGainKeys = new Track<float>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}