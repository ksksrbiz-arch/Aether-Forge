using Unity.Entities;
using UnityEngine;

namespace AetherForge.Factory
{
    public class InserterArmAuthoring : MonoBehaviour
    {
        public GameObject Source;
        public GameObject Target;
        public ItemType FilterItem = ItemType.None;
        public float TransferInterval = 0.2f;

        public class Baker : Baker<InserterArmAuthoring>
        {
            public override void Bake(InserterArmAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var source = authoring.Source != null
                    ? GetEntity(authoring.Source, TransformUsageFlags.Dynamic)
                    : Entity.Null;
                var target = authoring.Target != null
                    ? GetEntity(authoring.Target, TransformUsageFlags.Dynamic)
                    : Entity.Null;

                AddComponent(entity, new InserterArm
                {
                    Source = source,
                    Target = target,
                    FilterItem = authoring.FilterItem,
                    TransferInterval = Mathf.Max(FactoryDefaults.InserterMinimumTransferInterval, authoring.TransferInterval),
                    Cooldown = 0f
                });
            }
        }
    }
}
