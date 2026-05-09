# AetherForge - Voxel Mesh & Editing Phase

## Progress
- [x] Foundation Core (merged)
- [x] Voxel mesh generation (greedy-style starter)
- [x] WorldManager with chunk loading
- [x] Player block breaking + placing (raycast)

## How to Test
1. Create a new scene
2. Add WorldManager + assign ChunkPrefab (empty GameObject with VoxelChunk + VoxelMeshGenerator + MeshFilter + MeshRenderer)
3. Add PlayerInteraction to your player/camera
4. Play and left/right click to edit the world!

**Next**: Proper terrain generation + optimized meshing + DOTS factory integration