using UnityEngine;

namespace AetherForge.Factory
{
    public enum ItemType
    {
        None = 0,
        IronOre = 1,
        CopperOre = 2,
        IronPlate = 10,
        Gear = 11,
        // Add more as needed
    }

    [System.Serializable]
    public class Item
    {
        public ItemType Type;
        public int Quantity = 1;

        public Item(ItemType type, int qty = 1)
        {
            Type = type;
            Quantity = qty;
        }
    }
}