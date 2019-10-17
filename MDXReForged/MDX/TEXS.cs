using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class TEXS : BaseChunk, IReadOnlyList<Texture>
    {
        private readonly Texture[] Textures;

        public TEXS(BinaryReader br, uint version) : base(br, version)
        {
            Textures = new Texture[Size / 268];
            for (int i = 0; i < Textures.Length; i++)
                Textures[i] = new Texture(br);
        }

        public Texture this[int index] => Textures[index];

        public int Count => Textures.Length;

        public IEnumerator<Texture> GetEnumerator() => Textures.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Textures.AsEnumerable().GetEnumerator();
    }

    public class Texture
    {
        public uint ReplaceableId;
        public string Image;
        public TEXFLAGS Flags;

        public Texture(BinaryReader br)
        {
            ReplaceableId = br.ReadUInt32();
            Image = br.ReadCString(Constants.SizeFileName);
            Flags = (TEXFLAGS)br.ReadUInt32();
        }
    }
}