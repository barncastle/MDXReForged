using System;
using System.IO;

namespace MDXReForged.MDX
{
    public class BaseChunk
    {
        public readonly string Type;
        public readonly uint Size;
        protected readonly uint Version;

        public BaseChunk(BinaryReader br, uint version)
        {
            Type = br.ReadString(4);
            Size = br.ReadUInt32();
            Version = version;
        }
    }
}