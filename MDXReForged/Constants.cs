using System;

namespace MDXReForged
{
    public static class Constants
    {
        public const int SizeTag = 4;
        public const int SizeName = 80;
        public const int SizeFileName = 260;
    }

    #region ENUMS

    public enum LIGHT_TYPE
    {
        LIGHTTYPE_OMNI = 0x0,
        LIGHTTYPE_DIRECT = 0x1,
        LIGHTTYPE_AMBIENT = 0x2,
        NUM_MDL_LIGHT_TYPES = 0x3,
    }

    public enum GEOM_SHAPE
    {
        SHAPE_BOX = 0x0,
        SHAPE_CYLINDER = 0x1,
        SHAPE_SPHERE = 0x2,
        SHAPE_PLANE = 0x3,
        NUM_SHAPES = 0x4,
    }

    public enum MDLTEXOP
    {
        TEXOP_LOAD = 0x0,
        TEXOP_TRANSPARENT = 0x1,
        TEXOP_BLEND = 0x2,
        TEXOP_ADD = 0x3,
        TEXOP_ADD_ALPHA = 0x4,
        TEXOP_MODULATE = 0x5,
        TEXOP_MODULATE2X = 0x6,
        NUMTEXOPS = 0x7,
    }

    [Flags]
    public enum MDLGEO
    {
        MODEL_GEO_UNSHADED = 0x1,
        MODEL_GEO_SPHERE_ENV_MAP = 0x2,  // unused
        MODEL_GEO_WRAPWIDTH = 0x4,       // unused
        MODEL_GEO_WRAPHEIGHT = 0x8,      // unused
        MODEL_GEO_TWOSIDED = 0x10,
        MODEL_GEO_UNFOGGED = 0x20,
        MODEL_GEO_NO_DEPTH_TEST = 0x40,
        MODEL_GEO_NO_DEPTH_SET = 0x80,
    }

    public enum PARTICLE_BLEND_MODE
    {
        PBM_BLEND = 0x0,
        PBM_ADD = 0x1,
        PBM_MODULATE = 0x2,
        PBM_MODULATE_2X = 0x3,
        PBM_ALPHA_KEY = 0x4,
        NUM_PARTICLE_BLEND_MODES = 0x5,
    }

    public enum PARTICLE_TYPE
    {
        PT_HEAD = 0x0,
        PT_TAIL = 0x1,
        PT_BOTH = 0x2,
        NUM_PARTICLE_TYPES = 0x3,
    }

    public enum PARTICLE_EMITTER_TYPE
    {
        PET_BASE = 0x0,
        PET_PLANE = 0x1,
        PET_SPHERE = 0x2,
        PET_SPLINE = 0x3,
        NUM_PARTICLE_EMITTER_TYPES = 0x4,
    }

    public enum MDLTRACKTYPE
    {
        TRACK_NO_INTERP = 0x0,
        TRACK_LINEAR = 0x1,
        TRACK_HERMITE = 0x2,
        TRACK_BEZIER = 0x3,
        NUM_TRACK_TYPES = 0x4,
    }

    [Flags]
    public enum TEXFLAGS : uint
    {
        WRAPWIDTH = 1,
        WRAPHEIGHT = 2
    }

    [Flags]
    public enum GENOBJECTFLAGS : uint
    {
        DONT_INHERIT_TRANSLATION = 0x00000001,
        DONT_INHERIT_SCALING = 0x00000002,
        DONT_INHERIT_ROTATION = 0x00000004,
        BILLBOARD = 0x00000008,
        BILLBOARD_LOCK_X = 0x00000010,
        BILLBOARD_LOCK_Y = 0x00000020,
        BILLBOARD_LOCK_Z = 0x00000040,
        CAMERA_ANCHORED = 0x00000080,
        GENOBJECT_MDLBONESECTION = 0x00000100,
        GENOBJECT_MDLLIGHTSECTION = 0x00000200,
        GENOBJECT_MDLEVENTSECTION = 0x00000400,
        GENOBJECT_MDLATTACHMENTSECTION = 0x00000800,
        GENOBJECT_MDLPARTICLEEMITTER = 0x00001000,
        GENOBJECT_MDLHITTESTSHAPE = 0x00002000,
        GENOBJECT_MDLRIBBONEMITTER = 0x00004000,
        EMITTER_USES_MDL = 0x00008000,
        UNSHADED = 0x00008000,
        EMITTER_USES_TGA = 0x00010000,
        SORT_PRIMITIVES_FAR_Z = 0x00010000,
        LINE_EMITTER = 0x00020000,
        PARTICLE_UNFOGGED = 0x00040000,
        PARTICLE_USE_MODEL_SPACE = 0x00080000,
        PARTICLE_XYQUADS = 0x00100000,
    }

    #endregion ENUMS
}