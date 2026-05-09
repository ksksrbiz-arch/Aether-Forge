using Unity.Burst;
using Unity.Entities;

namespace AetherForge.Factory
{
    [BurstCompile]
    public partial struct ConveyorMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (conveyor, itemStack) in SystemAPI.Query<RefRO<ConveyorBelt>, RefRW<ItemStack>>())
            {
                if (itemStack.ValueRO.Count <= 0)
                {
                    itemStack.ValueRW.Progress = 0f;
                    continue;
                }

                itemStack.ValueRW.Progress += conveyor.ValueRO.Speed * deltaTime;
                if (itemStack.ValueRO.Progress >= 1f)
                    itemStack.ValueRW.Progress -= 1f;
            }
        }
    }
}
