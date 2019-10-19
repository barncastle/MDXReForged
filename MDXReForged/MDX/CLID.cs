using MDXReForged.Structs;
using System.IO;

namespace MDXReForged.MDX
{
    public class CLID : EnumerableBaseChunk<CollisionShape>
    {
        public CLID(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new CollisionShape(br));
        }
    }

    public class CollisionShape : GenObject
    {
        public GEOM_SHAPE Type;
        public CBox Box;
        public CCylinder Cylinder;
        public CSphere Sphere;
        public CPlane Plane;

        public CollisionShape(BinaryReader br)
        {
            ObjSize = br.ReadUInt32();
            Name = br.ReadCString(Constants.SizeName);
            ObjectId = br.ReadInt32();
            ParentId = br.ReadInt32();
            Flags = (GENOBJECTFLAGS)br.ReadUInt32();

            LoadTracks(br);

            Type = (GEOM_SHAPE)br.ReadUInt32();
            switch (Type)
            {
                case GEOM_SHAPE.SHAPE_BOX:
                    Box = new CBox(br);
                    break;

                case GEOM_SHAPE.SHAPE_CYLINDER:
                    Cylinder = new CCylinder(br);
                    break;

                case GEOM_SHAPE.SHAPE_PLANE:
                    Plane = new CPlane(br);
                    break;

                case GEOM_SHAPE.SHAPE_SPHERE:
                    Sphere = new CSphere(br);
                    break;
            }
        }
    }
}