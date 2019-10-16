using MDXReForged.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class Model
    {
        public readonly string BaseFile;
        public readonly string Magic;
        public IReadOnlyList<BaseChunk> Chunks;
        public IReadOnlyList<GenObject> Hierachy;

        public string Name => Get<MODL>().Name;
        public string AnimationFile => Get<MODL>().AnimationFile;
        public CExtent Bounds => Get<MODL>().Bounds;
        public uint BlendTime => Get<MODL>().BlendTime;

        public uint Version { get; private set; } = 0;

        public bool Has<T>() => Chunks?.Any(x => x.GetType() == typeof(T)) ?? false;

        public T Get<T>() where T : class => (T)(object)(Chunks.FirstOrDefault(x => x.GetType() == typeof(T)));

        public Model(string file)
        {
            BaseFile = file;

            var chunks = new List<BaseChunk>();
            using (var fs = new FileInfo(file).OpenRead())
            using (var br = new BinaryReader(fs))
            {
                Magic = br.ReadString(4);
                while (!br.AtEnd())
                    ReadChunk(br, chunks);
            }

            Chunks = chunks;
            PopulateHierachy();
        }

        private void ReadChunk(BinaryReader br, List<BaseChunk> chunks)
        {
            // no point parsing last 8 bytes as it's either padding or an empty chunk
            if (br.BaseStream.Length - br.BaseStream.Position <= 8)
                return;

            string tag = br.ReadString(4);
            if (typeLookup.ContainsKey(tag))
            {
                br.BaseStream.Position -= 4;
                chunks.Add((BaseChunk)Activator.CreateInstance(typeLookup[tag], br, Version));

                if (tag == "VERS")
                    Version = ((VERS)chunks.Last()).Version;
            }
            else
            {
                throw new Exception($"Unknown type {tag}");
            }
        }

        private void PopulateHierachy()
        {
            var hierachy = new List<GenObject>();

            // inherits MDLGENOBJECT
            foreach (var chunk in Chunks)
                if (chunk is IReadOnlyCollection<GenObject> collection)
                    hierachy.AddRange(collection);

            hierachy.Sort((x, y) => x.ObjectId.CompareTo(y.ObjectId));
            Hierachy = hierachy;
        }

        private static readonly Dictionary<string, Type> typeLookup = new Dictionary<string, Type>
        {
            { "VERS", typeof(VERS) },
            { "MODL", typeof(MODL) },
            { "SEQS", typeof(SEQS) },
            { "MTLS", typeof(MTLS) },
            { "TEXS", typeof(TEXS) },
            { "GEOS", typeof(GEOS) },
            { "BONE", typeof(BONE) },
            { "HELP", typeof(HELP) },
            { "ATCH", typeof(ATCH) },
            { "PIVT", typeof(PIVT) },
            { "CAMS", typeof(CAMS) },
            { "EVTS", typeof(EVTS) },
            { "CLID", typeof(CLID) },
            { "GLBS", typeof(GLBS) },
            { "GEOA", typeof(GEOA) },
            { "PRE2", typeof(PRE2) },
            { "RIBB", typeof(RIBB) },
            { "LITE", typeof(LITE) },
            { "TXAN", typeof(TXAN) },
            { "SNDS", typeof(SNDS) },
            { "BPOS", typeof(BPOS) },
            { "FAFX", typeof(FAFX) },
            { "PREM", typeof(PREM) },
            { "CORN", typeof(CORN) },
        };
    }
}