using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class PIVT : BaseChunk, IReadOnlyList<CVector3>
    {
        private readonly CVector3[] PivotPoints;

        public PIVT(BinaryReader br, uint version) : base(br, version)
        {
            PivotPoints = new CVector3[Size / 0xC];
            for (int i = 0; i < PivotPoints.Length; i++)
                PivotPoints[i] = new CVector3(br);
        }

        public CVector3 this[int index] => PivotPoints[index];

        public int Count => PivotPoints.Length;

        public IEnumerator<CVector3> GetEnumerator() => PivotPoints.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => PivotPoints.AsEnumerable().GetEnumerator();
    }
}