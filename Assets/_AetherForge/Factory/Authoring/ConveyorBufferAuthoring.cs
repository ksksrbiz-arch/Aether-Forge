using Unity.Entities;
using UnityEngine;

namespace AetherForge.Factory
{
    public class ConveyorBufferAuthoring : MonoBehaviour
    {
        public float Speed = 1f;
        public Direction Direction = Direction.East;
        public ItemType StartingItem = ItemType.None;
        public int StartingCount;
        public int Capacity = 8;

        public class Baker : Baker<ConveyorBufferAuthoring>
        {
            public override void Bake(ConveyorBufferAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ConveyorBelt
                {
                    Speed = Mathf.Max(0f, authoring.Speed),
                    Direction = authoring.Direction
                });

                AddComponent(entity, new ItemStack
                {
                    Item = authoring.StartingCount > 0 ? authoring.StartingItem : ItemType.None,
                    Count = Mathf.Max(0, authoring.StartingCount),
                    Capacity = Mathf.Max(1, authoring.Capacity),
                    Progress = 0f
                });
            }
        }
    }
}
