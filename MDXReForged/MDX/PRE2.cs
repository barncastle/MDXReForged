using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class PRE2 : BaseChunk, IReadOnlyList<ParticleEmitter2>
    {
        private readonly List<ParticleEmitter2> ParticleEmitter2s = new List<ParticleEmitter2>();

        public PRE2(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                ParticleEmitter2s.Add(new ParticleEmitter2(br));
        }

        public ParticleEmitter2 this[int index] => ParticleEmitter2s[index];

        public int Count => ParticleEmitter2s.Count;

        public IEnumerator<ParticleEmitter2> GetEnumerator() => ParticleEmitter2s.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ParticleEmitter2s.AsEnumerable().GetEnumerator();
    }

    public class ParticleEmitter2 : GenObject
    {
        public uint TotalSize;

        public int NodeSize;
        public PARTICLE_EMITTER_TYPE Type; //PARTICLE_EMITTER_TYPE
        public float Speed;
        public float Variation;
        public float Latitude;
        public float Longitude;
        public float Gravity;
        public float Lifespan;
        public float EmissionRate;
        public float Width;
        public float Length;
        public float ZSource;
        public PARTICLE_BLEND_MODE BlendMode;
        public PARTICLE_TYPE ParticleType; //PARTICLE_TYPE
        public int Rows;
        public int Cols;
        public float TailLength;
        public float MiddleTime;
        public CVector3 StartColor;
        public CVector3 MiddleColor;
        public CVector3 EndColor;
        public float StartAlpha;
        public float MiddleAlpha;
        public float EndAlpha;
        public float StartScale;
        public float MiddleScale;
        public float EndScale;
        public uint LifespanUVAnimStart;
        public uint LifespanUVAnimEnd;
        public uint LifespanUVAnimRepeat;
        public uint DecayUVAnimStart;
        public uint DecayUVAnimEnd;
        public uint DecayUVAnimRepeat;
        public uint TailUVAnimStart;
        public uint TailUVAnimEnd;
        public uint TailUVAnimRepeat;
        public uint TailDecayUVAnimStart;
        public uint TailDecayUVAnimEnd;
        public uint TailDecayUVAnimRepeat;
        public uint Squirts;
        public uint TextureId;
        public int PriorityPlane;
        public uint ReplaceableId;
        public string GeometryMdl;
        public string RecursionMdl;
        public float TwinkleFPS;
        public float TwinkleOnOff;
        public float TwinkleScaleMin;
        public float TwinkleScaleMax;
        public float IvelScale;
        public CVector2 TumbleX;
        public CVector2 TumbleY;
        public CVector2 TumbleZ;
        public float Drag;
        public float Spin;
        public CVector3 WindVector;
        public float WindTime;
        public float FollowSpeed1;
        public float FollowScale1;
        public float FollowSpeed2;
        public float FollowScale2;

        public Track<float> SpeedKeys;
        public Track<float> VariationKeys;
        public Track<float> LatitudeKeys;
        public Track<float> LongitudeKeys;
        public Track<float> ZSourceKeys;
        public Track<float> LifespanKeys;
        public Track<float> GravityKeys;
        public Track<float> EmissionRateKeys;
        public Track<float> WidthKeys;
        public Track<float> LengthKeys;
        public Track<float> VisibilityKeys;

        public ParticleEmitter2(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            Speed = br.ReadSingle();
            Variation = br.ReadSingle();
            Latitude = br.ReadSingle();
            Gravity = br.ReadSingle();
            Lifespan = br.ReadSingle();
            EmissionRate = br.ReadSingle();
            Length = br.ReadSingle();
            Width = br.ReadSingle();
            BlendMode = (PARTICLE_BLEND_MODE)br.ReadUInt32();
            Rows = br.ReadInt32();
            Cols = br.ReadInt32();
            ParticleType = (PARTICLE_TYPE)br.ReadInt32();
            TailLength = br.ReadSingle();
            MiddleTime = br.ReadSingle();

            StartColor = new CVector3(br);
            MiddleColor = new CVector3(br);
            EndColor = new CVector3(br);
            StartAlpha = br.ReadByte() / 255f;
            MiddleAlpha = br.ReadByte() / 255f;
            EndAlpha = br.ReadByte() / 255f;
            StartScale = br.ReadSingle();
            MiddleScale = br.ReadSingle();
            EndScale = br.ReadSingle();

            LifespanUVAnimStart = br.ReadUInt32();
            LifespanUVAnimEnd = br.ReadUInt32();
            LifespanUVAnimRepeat = br.ReadUInt32();

            DecayUVAnimStart = br.ReadUInt32();
            DecayUVAnimEnd = br.ReadUInt32();
            DecayUVAnimRepeat = br.ReadUInt32();

            TailUVAnimStart = br.ReadUInt32();
            TailUVAnimEnd = br.ReadUInt32();
            TailUVAnimRepeat = br.ReadUInt32();

            TailDecayUVAnimStart = br.ReadUInt32();
            TailDecayUVAnimEnd = br.ReadUInt32();
            TailDecayUVAnimRepeat = br.ReadUInt32();

            TextureId = br.ReadUInt32();
            Squirts = br.ReadUInt32();  // 1 for footsteps and impact spell effects
            PriorityPlane = br.ReadInt32();
            ReplaceableId = br.ReadUInt32();

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KP2S": SpeedKeys = new Track<float>(br); break;
                    case "KP2R": VariationKeys = new Track<float>(br); break;
                    case "KP2G": GravityKeys = new Track<float>(br); break;
                    case "KP2W": WidthKeys = new Track<float>(br); break;
                    case "KP2N": LengthKeys = new Track<float>(br); break;
                    case "KP2V": VisibilityKeys = new Track<float>(br); break;
                    case "KP2E": EmissionRateKeys = new Track<float>(br); break;
                    case "KP2L": LatitudeKeys = new Track<float>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}