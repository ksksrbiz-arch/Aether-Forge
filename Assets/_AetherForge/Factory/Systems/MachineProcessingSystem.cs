using Unity.Burst;
using Unity.Entities;

namespace AetherForge.Factory
{
    [BurstCompile]
    public partial struct MachineProcessingSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (machine, recipe, inventory) in
                     SystemAPI.Query<RefRW<Machine>, RefRO<MachineRecipe>, RefRW<MachineInventory>>())
            {
                if (inventory.ValueRO.InputCount < recipe.ValueRO.InputCount ||
                    inventory.ValueRO.OutputCount + recipe.ValueRO.OutputCount > inventory.ValueRO.OutputCapacity)
                {
                    machine.ValueRW.CurrentProgress = 0f;
                    continue;
                }

                machine.ValueRW.CurrentProgress += deltaTime;
                if (machine.ValueRO.CurrentProgress < recipe.ValueRO.ProcessingTime)
                    continue;

                machine.ValueRW.CurrentProgress = 0f;
                inventory.ValueRW.InputCount -= recipe.ValueRO.InputCount;
                if (inventory.ValueRO.InputCount <= 0)
                {
                    inventory.ValueRW.InputCount = 0;
                    inventory.ValueRW.InputType = ItemType.None;
                }

                if (inventory.ValueRO.OutputCount == 0)
                    inventory.ValueRW.OutputType = recipe.ValueRO.OutputItem;

                inventory.ValueRW.OutputCount += recipe.ValueRO.OutputCount;
            }
        }
    }
}
