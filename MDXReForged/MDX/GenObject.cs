using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class GenObject
    {
        public uint ObjSize;
        public string Name;
        public int ObjectId;
        public int ParentId;
        public GENOBJECTFLAGS Flags;

        public Track<CVector3> TranslationKeys;
        public Track<CVector4> RotationKeys;
        public Track<CVector3> ScaleKeys;

        public void LoadTracks(BinaryReader br)
        {
            while (!br.AtEnd())
            {
                string tagname = br.ReadString(4);
                switch (tagname)
                {
                    case "KGTR":
                        TranslationKeys = new Track<CVector3>(br);
                        break;

                    case "KGRT":
                        RotationKeys = new Track<CVector4>(br);
                        break;

                    case "KGSC":
                        ScaleKeys = new Track<CVector3>(br);
                        break;

                    default:
                        br.BaseStream.Position -= 4;
                        return;
                }
            }
        }
    }
}