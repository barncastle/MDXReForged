using System;
using System.IO;

namespace MDXReForged.MDX
{
    public class Track<T> where T : new()
    {
        public string Name;
        public uint NrOfTracks;
        public MDLTRACKTYPE InterpolationType;
        public int GlobalSequenceId;
        public CAnimatorNode<T>[] Nodes;

        public Track(BinaryReader br)
        {
            br.BaseStream.Position -= 4;

            Name = br.ReadString(Constants.SizeTag);
            NrOfTracks = br.ReadUInt32();
            InterpolationType = (MDLTRACKTYPE)br.ReadUInt32();
            GlobalSequenceId = br.ReadInt32();

            Nodes = new CAnimatorNode<T>[NrOfTracks];
            for (int i = 0; i < NrOfTracks; i++)
            {
                uint Time = br.ReadUInt32();
                T Value = CreateInstance(br);

                if (InterpolationType > MDLTRACKTYPE.TRACK_LINEAR)
                {
                    T InTangent = CreateInstance(br);
                    T OutTrangent = CreateInstance(br);

                    Nodes[i] = new CAnimatorNode<T>(Time, Value, InTangent, OutTrangent);
                }
                else
                {
                    Nodes[i] = new CAnimatorNode<T>(Time, Value);
                }
            }
        }

        private T CreateInstance(BinaryReader br)
        {
            switch (typeof(T).Name)
            {
                case "Single":
                    return (T)(object)br.ReadSingle();

                case "Int32":
                    return (T)(object)br.ReadInt32();

                default:
                    return (T)Activator.CreateInstance(typeof(T), br);
            }
        }
    }

    public class CAnimatorNode<T>
    {
        public uint Time;
        public T Value;
        public T InTangent;
        public T OutTangent;

        public CAnimatorNode(uint Time, T Value)
        {
            this.Time = Time;
            this.Value = Value;
        }

        public CAnimatorNode(uint Time, T Value, T InTangent, T OutTangent)
        {
            this.Time = Time;
            this.Value = Value;
            this.InTangent = InTangent;
            this.OutTangent = OutTangent;
        }
    }
}