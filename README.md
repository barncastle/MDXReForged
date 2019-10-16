# MDX ReForged

Experiments with reading the new Warcraft 3: Reforged MDX files. 

The project is fully compliant with all of the models used in the current client (as of time of writing) although it may not be fully compliant with the full spec (some chunks are never used).

####Notable changes:

Changes appear to have been made as per below. Most of which are wrapped in a version check by the client as to preserve backwards compatibility.

**New BPOS Chunk** - Bind Positions

**New FAFX Chunk** - FaceFX, presumably used for the new facial animations

**New CORN Chunk** - PopcornFX particle emitter

**Changed GEOS** - Now contains a `LevelOfDetail` and `FilePath/Geoset` Name field

​	**New TANG Sub-Chunk** - Contains tangents, they are C4Vectors with `w` storing the handedness (just like Unity's implementation)

​	**New SKIN Sub-Chunk** - Contains bond indices and weights

**Changed MTLS Chunk** - Now contains a `Shader` file path field

​	**Changed LAYS Sub-Chunk** - Contains a new float and float track, presumably `EmissiveGain`