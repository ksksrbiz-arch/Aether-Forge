using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.World
{
    /// <summary>
    /// Basic voxel chunk (16x16x16 for now).
    /// Future: Integrate with DOTS for large worlds + mesh generation.
    /// </summary>
    public class VoxelChunk : MonoBehaviour
    {
        public const int Size = 16;
        public VoxelData[,,] Voxels = new VoxelData[Size, Size, Size];

        public Vector3Int ChunkPosition;

        private void Start()
        {
            // TODO: Generate terrain or load from disk
            Debug.Log($"Chunk initialized at {ChunkPosition}");
        }

        public void SetVoxel(int x, int y, int z, VoxelData data)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size) return;
            Voxels[x, y, z] = data;
            // TODO: Mark dirty for mesh rebuild
        }

        public VoxelData GetVoxel(int x, int y, int z)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size)
                return VoxelData.Air;
            return Voxels[x, y, z];
        }
    }
}