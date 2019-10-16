using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class GLBS : BaseChunk, IReadOnlyCollection<int>
    {
        private int[] Duration;

        public GLBS(BinaryReader br, uint version) : base(br)
        {
            Duration = new int[Size / 4];
            for (int i = 0; i < Duration.Length; i++)
                Duration[i] = br.ReadInt32();
        }

        public int Count => Duration.Length;

        public IEnumerator<int> GetEnumerator() => Duration.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Duration.AsEnumerable().GetEnumerator();
    }
}