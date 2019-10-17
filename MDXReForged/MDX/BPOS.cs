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
    public class BPOS : BaseChunk, IReadOnlyList<C34Matrix>
    {
        private readonly C34Matrix[] BindPositions;

        public BPOS(BinaryReader br, uint version) : base(br, version)
        {
            BindPositions = new C34Matrix[br.ReadInt32()];
            for (int i = 0; i < BindPositions.Length; i++)
                BindPositions[i] = new C34Matrix(br);
        }

        public C34Matrix this[int index] => BindPositions[index];

        public int Count => BindPositions.Length;

        public IEnumerator<C34Matrix> GetEnumerator() => BindPositions.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => BindPositions.AsEnumerable().GetEnumerator();
    }
}