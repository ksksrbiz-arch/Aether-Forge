using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.World
{
    /// <summary>
    /// Manages the voxel world: chunk loading, editing, persistence.
    /// This is the central system players interact with.
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        public int RenderDistance = 4;
        public GameObject ChunkPrefab;

        private Dictionary<Vector3Int, VoxelChunk> loadedChunks = new Dictionary<Vector3Int, VoxelChunk>();

        private void Start()
        {
            // Generate starting area
            for (int x = -RenderDistance; x <= RenderDistance; x++)
            for (int z = -RenderDistance; z <= RenderDistance; z++)
            {
                Vector3Int pos = new Vector3Int(x, 0, z);
                CreateChunk(pos);
            }
        }

        public VoxelChunk CreateChunk(Vector3Int position)
        {
            GameObject go = Instantiate(ChunkPrefab, position * VoxelChunk.Size, Quaternion.identity);
            VoxelChunk chunk = go.GetComponent<VoxelChunk>();
            chunk.ChunkPosition = position;

            // TODO: Proper terrain generation (Perlin, etc.)
            // For now: simple flat world
            for (int x = 0; x < VoxelChunk.Size; x++)
            for (int z = 0; z < VoxelChunk.Size; z++)
            {
                chunk.SetVoxel(x, 0, z, new VoxelData { Type = 1 }); // Stone
                chunk.SetVoxel(x, 1, z, new VoxelData { Type = 2 }); // Dirt
            }

            VoxelMeshGenerator meshGen = go.AddComponent<VoxelMeshGenerator>();
            meshGen.GenerateMesh();

            loadedChunks[position] = chunk;
            return chunk;
        }

        public void SetBlock(Vector3Int worldPos, VoxelData data)
        {
            Vector3Int chunkPos = new Vector3Int(
                Mathf.FloorToInt(worldPos.x / VoxelChunk.Size),
                Mathf.FloorToInt(worldPos.y / VoxelChunk.Size),
                Mathf.FloorToInt(worldPos.z / VoxelChunk.Size));

            if (loadedChunks.TryGetValue(chunkPos, out VoxelChunk chunk))
            {
                int lx = worldPos.x % VoxelChunk.Size;
                int ly = worldPos.y % VoxelChunk.Size;
                int lz = worldPos.z % VoxelChunk.Size;

                chunk.SetVoxel(lx, ly, lz, data);

                // Rebuild mesh
                VoxelMeshGenerator gen = chunk.GetComponent<VoxelMeshGenerator>();
                if (gen != null) gen.GenerateMesh();
            }
        }
    }
}