# MDX ReForged

Experiments with reading the new Warcraft 3: Reforged MDX files. 

The project *should be* compliant with all of the models used in the current client (as of time of writing) although it may not be with the full spec (some chunks are never used).

#### Notable changes:

Changes appear to have been made as per below. Most of which are wrapped in a version check by the client as to preserve backwards compatibility.

**New BPOS Chunk** - Bind Positions

**New FAFX Chunk** - FaceFX, used for the new facial animations, see vendor docs

**New CORN Chunk** - PopcornFX particle emitter, see vendor docs

**Changed GEOS Chunk** - Now contains a `LevelOfDetail` and a `FilePath/GeosetName` field

- **New TANG Sub-Chunk** - Tangents. These are C4Vectors with `w` storing the handedness (see Unity's docs)

- **New SKIN Sub-Chunk** - Contains bone indices and weights

**Changed MTLS Chunk** - Now contains a `Shader` file path field

- **Changed LAYS Sub-Chunk** - Contains a new float and float track, presumed to be `EmissiveGain`
