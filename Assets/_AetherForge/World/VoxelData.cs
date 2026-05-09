using UnityEngine;

namespace AetherForge.World
{
    /// <summary>
    /// Represents a single voxel in the world.
    /// Used by chunk system for terrain and building.
    /// </summary>
    [System.Serializable]
    public struct VoxelData
    {
        public byte Type;           // 0 = Air, 1 = Stone, 2 = Dirt, etc.
        public byte LightLevel;
        public ushort Metadata;     // For future use (e.g. rotation, state)

        public static VoxelData Air => new VoxelData { Type = 0 };

        public bool IsSolid => Type != 0;
    }
}