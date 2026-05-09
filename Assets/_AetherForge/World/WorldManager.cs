using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.World
{
    /// <summary>
    /// Manages the voxel world: chunk loading, editing, persistence.
    /// Now with procedural terrain (Perlin-based for mobile performance).
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        public int RenderDistance = 4;
        public GameObject ChunkPrefab;
        public float TerrainScale = 0.1f;
        public int MaxHeight = 8;

        private Dictionary<Vector3Int, VoxelChunk> loadedChunks = new Dictionary<Vector3Int, VoxelChunk>();

        private void Start()
        {
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

            GenerateTerrain(chunk, position);

            VoxelMeshGenerator meshGen = go.AddComponent<VoxelMeshGenerator>();
            meshGen.GenerateMesh();

            loadedChunks[position] = chunk;
            return chunk;
        }

        private void GenerateTerrain(VoxelChunk chunk, Vector3Int chunkPos)
        {
            for (int x = 0; x < VoxelChunk.Size; x++)
            for (int z = 0; z < VoxelChunk.Size; z++)
            {
                float worldX = (chunkPos.x * VoxelChunk.Size + x) * TerrainScale;
                float worldZ = (chunkPos.z * VoxelChunk.Size + z) * TerrainScale;

                float height = Mathf.PerlinNoise(worldX, worldZ) * MaxHeight;
                int surfaceY = Mathf.FloorToInt(height);

                for (int y = 0; y <= surfaceY && y < VoxelChunk.Size; y++)
                {
                    byte type;
                    if (y == surfaceY) type = 3; // Grass
                    else if (y > surfaceY - 2) type = 2; // Dirt
                    else type = 1; // Stone

                    if (y < surfaceY - 1 && Random.value < 0.05f) type = 4; // Ore

                    chunk.SetVoxel(x, y, z, new VoxelData { Type = type });
                }

                if (surfaceY > 0)
                    chunk.SetVoxel(x, 0, z, new VoxelData { Type = 5 }); // Bedrock
            }
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

                VoxelMeshGenerator gen = chunk.GetComponent<VoxelMeshGenerator>();
                if (gen != null) gen.GenerateMesh();
            }
        }

        public void SetRenderDistance(int distance)
        {
            RenderDistance = Mathf.Clamp(distance, 2, 8);
        }
    }
}