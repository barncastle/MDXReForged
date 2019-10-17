using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class CAMS : BaseChunk, IReadOnlyList<Camera>
    {
        private readonly List<Camera> Cameras = new List<Camera>();

        public CAMS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Cameras.Add(new Camera(br));
        }

        public Camera this[int index] => Cameras[index];

        public int Count => Cameras.Count;

        public IEnumerator<Camera> GetEnumerator() => Cameras.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Cameras.AsEnumerable().GetEnumerator();
    }

    public class Camera
    {
        public uint TotalSize;
        public string Name;
        public CVector3 Pivot;
        public float FieldOfView;
        public float FarClip;
        public float NearClip;
        public CVector3 TargetPosition;

        public Track<CVector3> TranslationKeys;
        public Track<CVector3> TargetTranslationKeys;
        public Track<float> RotationKeys;

        public Camera(BinaryReader br)
        {
            long end = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            Name = br.ReadCString(Constants.SizeName);
            Pivot = new CVector3(br);
            FieldOfView = br.ReadSingle();
            FarClip = br.ReadSingle();
            NearClip = br.ReadSingle();
            TargetPosition = new CVector3(br);

            while (br.BaseStream.Position < end && !br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KCTR": TranslationKeys = new Track<CVector3>(br); break;
                    case "KCRL": RotationKeys = new Track<float>(br); break;
                    case "KTTR": TargetTranslationKeys = new Track<CVector3>(br); break;
                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}