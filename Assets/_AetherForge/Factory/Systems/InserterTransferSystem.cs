using Unity.Burst;
using Unity.Entities;

namespace AetherForge.Factory
{
    [BurstCompile]
    public partial struct InserterTransferSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var itemStacks = SystemAPI.GetComponentLookup<ItemStack>();
            var machineInventories = SystemAPI.GetComponentLookup<MachineInventory>();

            foreach (var inserter in SystemAPI.Query<RefRW<InserterArm>>())
            {
                inserter.ValueRW.Cooldown -= deltaTime;
                if (inserter.ValueRO.Cooldown > 0f)
                    continue;

                if (!TryPeekSourceItem(in inserter.ValueRO, itemStacks, machineInventories, out var itemType))
                    continue;

                if (inserter.ValueRO.FilterItem != ItemType.None && inserter.ValueRO.FilterItem != itemType)
                    continue;

                if (!TryInsertTarget(in inserter.ValueRO, itemType, ref itemStacks, ref machineInventories))
                    continue;

                RemoveFromSource(in inserter.ValueRO, ref itemStacks, ref machineInventories);
                inserter.ValueRW.Cooldown = inserter.ValueRO.TransferInterval <= 0f ? 0.1f : inserter.ValueRO.TransferInterval;
            }
        }

        private static bool TryPeekSourceItem(
            in InserterArm inserter,
            ComponentLookup<ItemStack> itemStacks,
            ComponentLookup<MachineInventory> machineInventories,
            out ItemType itemType)
        {
            itemType = ItemType.None;

            if (itemStacks.HasComponent(inserter.Source))
            {
                var stack = itemStacks[inserter.Source];
                if (stack.Count <= 0 || stack.ItemType == ItemType.None)
                    return false;

                itemType = stack.ItemType;
                return true;
            }

            if (machineInventories.HasComponent(inserter.Source))
            {
                var inventory = machineInventories[inserter.Source];
                if (inventory.OutputCount <= 0 || inventory.OutputType == ItemType.None)
                    return false;

                itemType = inventory.OutputType;
                return true;
            }

            return false;
        }

        private static bool TryInsertTarget(
            in InserterArm inserter,
            ItemType itemType,
            ref ComponentLookup<ItemStack> itemStacks,
            ref ComponentLookup<MachineInventory> machineInventories)
        {
            if (itemStacks.HasComponent(inserter.Target))
            {
                var stack = itemStacks[inserter.Target];
                var capacity = stack.Capacity <= 0 ? 8 : stack.Capacity;
                if (stack.Count >= capacity)
                    return false;

                if (stack.Count > 0 && stack.ItemType != itemType)
                    return false;

                if (stack.Count == 0)
                    stack.ItemType = itemType;

                stack.Count += 1;
                itemStacks[inserter.Target] = stack;
                return true;
            }

            if (machineInventories.HasComponent(inserter.Target))
            {
                var inventory = machineInventories[inserter.Target];
                var capacity = inventory.InputCapacity <= 0 ? 16 : inventory.InputCapacity;
                if (inventory.InputCount >= capacity)
                    return false;

                if (inventory.InputCount > 0 && inventory.InputType != itemType)
                    return false;

                if (inventory.InputCount == 0)
                    inventory.InputType = itemType;

                inventory.InputCount += 1;
                machineInventories[inserter.Target] = inventory;
                return true;
            }

            return false;
        }

        private static void RemoveFromSource(
            in InserterArm inserter,
            ref ComponentLookup<ItemStack> itemStacks,
            ref ComponentLookup<MachineInventory> machineInventories)
        {
            if (itemStacks.HasComponent(inserter.Source))
            {
                var stack = itemStacks[inserter.Source];
                stack.Count -= 1;
                if (stack.Count <= 0)
                {
                    stack.Count = 0;
                    stack.ItemType = ItemType.None;
                }

                itemStacks[inserter.Source] = stack;
                return;
            }

            if (machineInventories.HasComponent(inserter.Source))
            {
                var inventory = machineInventories[inserter.Source];
                inventory.OutputCount -= 1;
                if (inventory.OutputCount <= 0)
                {
                    inventory.OutputCount = 0;
                    inventory.OutputType = ItemType.None;
                }

                machineInventories[inserter.Source] = inventory;
            }
        }
    }
}
