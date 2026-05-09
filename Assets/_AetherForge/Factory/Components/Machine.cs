using Unity.Entities;

namespace AetherForge.Factory
{
    public struct Machine : IComponentData
    {
        public MachineType Type;
        public float CurrentProgress;
    }

    public struct MachineRecipe : IComponentData
    {
        public ItemType InputItem;
        public int InputCount;
        public ItemType OutputItem;
        public int OutputCount;
        public float ProcessingTime;
    }

    public struct MachineInventory : IComponentData
    {
        public ItemType InputType;
        public int InputCount;
        public int InputCapacity;
        public ItemType OutputType;
        public int OutputCount;
        public int OutputCapacity;
    }

    public enum MachineType
    {
        Smelter,
        Assembler
    }
}
