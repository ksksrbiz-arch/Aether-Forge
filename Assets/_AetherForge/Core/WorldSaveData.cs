using UnityEngine;
using System;
using System.Collections.Generic;

namespace AetherForge.Core
{
    [Serializable]
    public class ChunkSaveData
    {
        public Vector3Int Position;
        public byte[,,] VoxelTypes; // Flattened or 3D array

        public ChunkSaveData(Vector3Int pos, VoxelData[,,] voxels)
        {
            Position = pos;
            VoxelTypes = new byte[VoxelChunk.Size, VoxelChunk.Size, VoxelChunk.Size];
            for (int x = 0; x < VoxelChunk.Size; x++)
            for (int y = 0; y < VoxelChunk.Size; y++)
            for (int z = 0; z < VoxelChunk.Size; z++)
            {
                VoxelTypes[x, y, z] = voxels[x, y, z].Type;
            }
        }
    }

    [Serializable]
    public class WorldSaveData
    {
        public List<ChunkSaveData> Chunks = new List<ChunkSaveData>();
        public List<MachineSaveData> Machines = new List<MachineSaveData>();
        public float SaveTime;
    }

    [Serializable]
    public class MachineSaveData
    {
        public string Type; // "ConveyorBelt", "Inserter", "Assembler"
        public Vector3 Position;
        public Quaternion Rotation;
        // Add more fields as needed (e.g., recipe, inventory)
    }
}