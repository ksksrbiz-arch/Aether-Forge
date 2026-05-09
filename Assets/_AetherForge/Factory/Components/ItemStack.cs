using Unity.Entities;
using Unity.Mathematics;

public struct ItemStack : IComponentData
{
    public ItemType ItemType;
    public int Count;
    public float Progress; // For processing
}

public enum ItemType
{
    None = 0,
    IronOre,
    CopperOre,
    IronIngot,
    Gear,
    Circuit
}