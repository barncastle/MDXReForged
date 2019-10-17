using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    /// <summary>
    /// FaceFX
    /// </summary>
    public class FAFX : BaseChunk, IReadOnlyList<FaceFX>
    {
        private readonly List<FaceFX> FaceFXs = new List<FaceFX>();

        public FAFX(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                FaceFXs.Add(new FaceFX(br));
        }

        public FaceFX this[int index] => FaceFXs[index];

        public int Count => FaceFXs.Count;

        public IEnumerator<FaceFX> GetEnumerator() => FaceFXs.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => FaceFXs.AsEnumerable().GetEnumerator();
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