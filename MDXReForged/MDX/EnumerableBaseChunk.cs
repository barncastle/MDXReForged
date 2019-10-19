using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MDXReForged.MDX
{
    public class EnumerableBaseChunk<T> : BaseChunk, IReadOnlyList<T>
    {
        protected readonly List<T> Values = new List<T>();

        public EnumerableBaseChunk(BinaryReader br, uint version) : base(br, version)
        { }


        public T this[int index] => Values[index];

        public int Count => Values.Count;

        public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
    }
}
