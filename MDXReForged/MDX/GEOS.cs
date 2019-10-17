using MDXReForged.Structs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MDXReForged.MDX
{
    public class GEOS : BaseChunk, IReadOnlyList<Geoset>
    {
        private readonly List<Geoset> Geosets = new List<Geoset>();

        public GEOS(BinaryReader br, uint version) : base(br, version)
        {
            long end = br.BaseStream.Position + Size;
            while (br.BaseStream.Position < end)
                Geosets.Add(new Geoset(br, version));
        }

        public Geoset this[int index] => Geosets[index];

        public int Count => Geosets.Count;

        public IEnumerator<Geoset> GetEnumerator() => Geosets.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Geosets.AsEnumerable().GetEnumerator();
    }

    public class Geoset
    {
        public uint TotalSize;
        public uint NrOfVertices;
        public List<CVector3> Vertices = new List<CVector3>();
        public uint NrOfNormals;
        public List<CVector3> Normals = new List<CVector3>();
        public uint NrOfTexCoordSets;
        public List<CVector2>[] TexCoords;
        public uint NrOfTangents;
        public List<CVector4> Tangents = new List<CVector4>();

        //MDLPRIMITIVES
        public uint NrOfFaceTypeGroups;

        public List<uint> FaceTypes = new List<uint>();
        public uint NrOfFaceGroups;
        public List<uint> FaceGroups = new List<uint>();
        public uint NrOfFaceVertices;
        public List<CVertex> FaceVertices = new List<CVertex>();

        public uint NrOfVertexGroupIndices;
        public List<byte> VertexGroupIndices = new List<byte>();
        public uint NrOfMatrixGroups;
        public List<uint> MatrixGroups = new List<uint>();
        public uint NrOfMatrixIndexes;
        public List<uint> MatrixIndexes = new List<uint>();
        public uint NrOfBoneIndexes;
        public List<uint> BoneIndexes = new List<uint>();
        public uint SkinSize;
        public List<uint> BoneWeights = new List<uint>();

        public uint MaterialId;
        public CExtent Bounds;
        public uint SelectionGroup;
        public bool Unselectable;
        public uint LevelOfDetail;
        public string FilePath;

        public uint NrOfExtents;
        public List<CExtent> Extents = new List<CExtent>();

        public Geoset(BinaryReader br, uint version)
        {
            _ = br.BaseStream.Position + (TotalSize = br.ReadUInt32());

            //Vertices
            if (br.HasTag("VRTX"))
            {
                NrOfVertices = br.ReadUInt32();
                for (int i = 0; i < NrOfVertices; i++)
                    Vertices.Add(new CVector3(br));
            }

            //Normals
            if (br.HasTag("NRMS"))
            {
                NrOfNormals = br.ReadUInt32();
                for (int i = 0; i < NrOfNormals; i++)
                    Normals.Add(new CVector3(br));
            }

            //Face Group Type
            if (br.HasTag("PTYP"))
            {
                NrOfFaceTypeGroups = br.ReadUInt32();
                for (int i = 0; i < NrOfFaceTypeGroups; i++)
                    FaceTypes.Add(br.ReadUInt32());
            }

            //Face Groups
            if (br.HasTag("PCNT"))
            {
                NrOfFaceGroups = br.ReadUInt32();
                for (int i = 0; i < NrOfFaceGroups; i++)
                    FaceGroups.Add(br.ReadUInt32());
            }

            //Indexes
            if (br.HasTag("PVTX"))
            {
                NrOfFaceVertices = br.ReadUInt32();
                for (int i = 0; i < NrOfFaceVertices / 3; i++)
                    FaceVertices.Add(new CVertex(br));
            }

            //Vertex Groups
            if (br.HasTag("GNDX"))
            {
                NrOfVertexGroupIndices = br.ReadUInt32();
                VertexGroupIndices.AddRange(br.ReadBytes((int)NrOfVertexGroupIndices));
            }

            //Matrix Groups
            if (br.HasTag("MTGC"))
            {
                NrOfMatrixGroups = br.ReadUInt32();
                for (int i = 0; i < NrOfMatrixGroups; i++)
                    MatrixGroups.Add(br.ReadUInt32());
            }

            //Matrix Indexes
            if (br.HasTag("MATS"))
            {
                NrOfMatrixIndexes = br.ReadUInt32();
                for (int i = 0; i < NrOfMatrixIndexes; i++)
                    MatrixIndexes.Add(br.ReadUInt32());
            }

            MaterialId = br.ReadUInt32();
            SelectionGroup = br.ReadUInt32();
            Unselectable = br.ReadUInt32() == 1;

            if (version >= 900)
            {
                LevelOfDetail = br.ReadUInt32();
                FilePath = br.ReadCString(Constants.SizeName);
            }

            Bounds = new CExtent(br);

            //Extents
            NrOfExtents = br.ReadUInt32();
            for (int i = 0; i < NrOfExtents; i++)
                Extents.Add(new CExtent(br));

            //Tangents
            if (br.HasTag("TANG"))
            {
                NrOfTangents = br.ReadUInt32();
                for (int i = 0; i < NrOfTangents; i++)
                    Tangents.Add(new CVector4(br));
            }

            //Skin
            if (br.HasTag("SKIN"))
            {
                SkinSize = br.ReadUInt32();
                for (int i = 0; i < SkinSize / 8; i++)
                {
                    // looks like a C2iVector
                    BoneIndexes.Add(br.ReadUInt32());
                    BoneWeights.Add(br.ReadUInt32());
                }
            }

            //TexCoordSets
            if (br.HasTag("UVAS"))
            {
                NrOfTexCoordSets = br.ReadUInt32();
                TexCoords = new List<CVector2>[NrOfTexCoordSets];
            }

            //TexCoords
            for (int i = 0; i < NrOfTexCoordSets && br.HasTag("UVBS"); i++)
            {
                int NrOfTexCoords = br.ReadInt32();
                TexCoords[i] = new List<CVector2>(NrOfTexCoords);

                for (int j = 0; j < NrOfTexCoords; j++)
                    TexCoords[i].Add(new CVector2(br));
            }
        }
    }
}