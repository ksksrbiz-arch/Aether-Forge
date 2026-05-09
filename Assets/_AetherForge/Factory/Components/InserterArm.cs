using Unity.Entities;

namespace AetherForge.Factory
{
    public struct InserterArm : IComponentData
    {
        public Entity Source;
        public Entity Target;
        public ItemType FilterItem;
        public float TransferInterval;
        public float Cooldown;
    }
}
