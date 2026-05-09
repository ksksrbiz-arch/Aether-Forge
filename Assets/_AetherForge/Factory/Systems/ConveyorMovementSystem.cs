using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ConveyorMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        foreach (var (transform, conveyor, itemStack) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<ConveyorBelt>, RefRW<ItemStack>>())
        {
            // Simple movement simulation along belt
            // In full version would move items between positions
            itemStack.ValueRW.Progress += conveyor.ValueRO.Speed * deltaTime;
        }
    }
}