using System.IO;

namespace MDXReForged.Structs
{
    public class CExtent
    {
        public float Radius;
        public CBox Extent;

        public CExtent() => Extent = new CBox();

        public CExtent(BinaryReader br)
        {
            Radius = br.ReadSingle();
            Extent = new CBox(br);
        }

        public override string ToString() => $"R: {Radius}, Min: {Extent.Min.ToString()}, Max: {Extent.Max.ToString()}";
    }
}