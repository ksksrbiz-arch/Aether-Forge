using Unity.Entities;
using UnityEngine;

namespace AetherForge.Factory
{
    public class MachineRecipeAuthoring : MonoBehaviour
    {
        public MachineType MachineType = MachineType.Assembler;
        public RecipeDefinition Recipe;
        public int InputCapacity = 16;
        public int OutputCapacity = 16;

        public class Baker : Baker<MachineRecipeAuthoring>
        {
            public override void Bake(MachineRecipeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                var recipe = authoring.Recipe != null
                    ? authoring.Recipe.ToMachineRecipe()
                    : new MachineRecipe
                    {
                        InputItem = ItemType.IronOre,
                        InputCount = 1,
                        OutputItem = ItemType.IronPlate,
                        OutputCount = 1,
                        ProcessingTime = 1f
                    };

                AddComponent(entity, new Machine
                {
                    Type = authoring.MachineType,
                    CurrentProgress = 0f
                });

                AddComponent(entity, recipe);
                AddComponent(entity, new MachineInventory
                {
                    InputType = ItemType.None,
                    InputCount = 0,
                    InputCapacity = Mathf.Max(1, authoring.InputCapacity),
                    OutputType = ItemType.None,
                    OutputCount = 0,
                    OutputCapacity = Mathf.Max(1, authoring.OutputCapacity)
                });
            }
        }
    }
}
