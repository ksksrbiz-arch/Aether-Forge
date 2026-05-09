using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct MachineProcessingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        foreach (var machine in SystemAPI.Query<RefRW<Machine>>())
        {
            machine.ValueRW.CurrentProgress += deltaTime;
            if (machine.ValueRO.CurrentProgress >= machine.ValueRO.ProcessingTime)
            {
                // Process recipe (stub)
                machine.ValueRW.CurrentProgress = 0;
            }
        }
    }
}