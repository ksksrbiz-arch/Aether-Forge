using Unity.Entities;

namespace AetherForge.Factory
{
    public struct ItemStack : IComponentData
    {
        public ItemType StackItemType;
        public int Count;
        public int Capacity;
        public float Progress;
    }
}
