﻿using System.IO;

namespace MDXReForged.MDX
{
    public class PREM : EnumerableBaseChunk<ParticleEmitter>
    {
        public PREM(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new ParticleEmitter(br));
        }
    }

    public class ParticleEmitter : GenObject
    {
        public uint TotalSize;
        public float EmissionRate;
        public float Gravity;
        public float Longitude;
        public float Latitude;
        public string Path;
        public float Lifespan;
        public float Speed;

        public Track<float> EmissionKeys;
        public Track<float> GravityKeys;
        public Track<float> LongitudeKeys;
        public Track<float> LatitudeKeys;
        public Track<float> LifespanKeys;
        public Track<float> SpeedKeys;
        public Track<float> VisibilityKeys;

        public ParticleEmitter(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            EmissionRate = br.ReadSingle();
            Gravity = br.ReadSingle();
            Longitude = br.ReadSingle();
            Latitude = br.ReadSingle();
            Path = br.ReadCString(Constants.SizeFileName);
            Lifespan = br.ReadSingle();
            Speed = br.ReadSingle();

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KPEE": EmissionKeys = new Track<float>(br); break;
                    case "KPEG": GravityKeys = new Track<float>(br); break;
                    case "KPLN": LongitudeKeys = new Track<float>(br); break;
                    case "KPLT": LatitudeKeys = new Track<float>(br); break;
                    case "KPEL": LifespanKeys = new Track<float>(br); break;
                    case "KPES": SpeedKeys = new Track<float>(br); break;
                    case "KPEV": VisibilityKeys = new Track<float>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}