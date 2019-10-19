using System.IO;

namespace MDXReForged.MDX
{
    /// <summary>
    /// FaceFX
    /// </summary>
    public class FAFX : EnumerableBaseChunk<FaceFX>
    {
        public FAFX(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Values.Add(new FaceFX(br));
        }
    }

    public class FaceFX
    {
        public string Node;
        public string FilePath;

        public FaceFX(BinaryReader br)
        {
            Node = br.ReadCString(Constants.SizeName);
            FilePath = br.ReadCString(Constants.SizeFileName);
        }
    }
}