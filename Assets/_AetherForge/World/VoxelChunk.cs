using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.World
{
    /// <summary>
    /// Voxel chunk with mesh rebuilding support.
    /// </summary>
    public class VoxelChunk : MonoBehaviour
    {
        public const int Size = 16;
        public VoxelData[,,] Voxels = new VoxelData[Size, Size, Size];

        public Vector3Int ChunkPosition;

        private VoxelMeshGenerator meshGenerator;

        private void Awake()
        {
            meshGenerator = GetComponent<VoxelMeshGenerator>();
        }

        public void SetVoxel(int x, int y, int z, VoxelData data)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size) return;
            Voxels[x, y, z] = data;

            // Auto-rebuild mesh when changed
            if (meshGenerator != null)
                meshGenerator.GenerateMesh();
        }

        public VoxelData GetVoxel(int x, int y, int z)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size)
                return VoxelData.Air;
            return Voxels[x, y, z];
        }
    }
}