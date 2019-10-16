using System;
using System.IO;

namespace MDXReForged.MDX
{
    public class BaseChunk
    {
        public string Type;
        public uint Size;
        protected uint Version;

        public BaseChunk(BinaryReader br)
        {
            Type = br.ReadString(4);
            Size = br.ReadUInt32();
        }
    }
}