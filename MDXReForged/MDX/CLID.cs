using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class CLID : BaseChunk, IReadOnlyCollection<CollisionShape>
    {
        private List<CollisionShape> CollisionShapes = new List<CollisionShape>();

        public CLID(BinaryReader br, uint version) : base(br)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                CollisionShapes.Add(new CollisionShape(br));
        }

        public int Count => CollisionShapes.Count;

        public IEnumerator<CollisionShape> GetEnumerator() => CollisionShapes.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => CollisionShapes.AsEnumerable().GetEnumerator();
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