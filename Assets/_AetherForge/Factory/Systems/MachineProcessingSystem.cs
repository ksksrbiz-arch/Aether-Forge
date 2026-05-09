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
                var remainingInput = inventory.ValueRO.InputCount - recipe.ValueRO.InputCount;
                if (remainingInput < 0)
                    remainingInput = 0;

                inventory.ValueRW.InputCount = remainingInput;
                if (remainingInput == 0)
                {
                    inventory.ValueRW.InputType = ItemType.None;
                }

                if (inventory.ValueRO.OutputCount == 0)
                    inventory.ValueRW.OutputType = recipe.ValueRO.OutputItem;

                inventory.ValueRW.OutputCount += recipe.ValueRO.OutputCount;
            }
        }
    }
}
