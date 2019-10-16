using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    /// <summary>
    /// BindPose
    /// </summary>
    public class BPOS : BaseChunk, IReadOnlyCollection<C34Matrix>
    {
        private C34Matrix[] BindPositions;

        public BPOS(BinaryReader br, uint version) : base(br)
        {
            BindPositions = new C34Matrix[br.ReadInt32()];
            for (int i = 0; i < BindPositions.Length; i++)
                BindPositions[i] = new C34Matrix(br);
        }

        public int Count => BindPositions.Length;

        public IEnumerator<C34Matrix> GetEnumerator() => BindPositions.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => BindPositions.AsEnumerable().GetEnumerator();
    }
}