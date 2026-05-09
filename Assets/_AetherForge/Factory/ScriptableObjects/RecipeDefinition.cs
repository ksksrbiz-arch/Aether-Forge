using UnityEngine;

namespace AetherForge.Factory
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "AetherForge/Factory/Recipe")]
    public class RecipeDefinition : ScriptableObject
    {
        public string RecipeId = "recipe";
        public MachineType MachineType = MachineType.Assembler;
        public ItemType InputItem = ItemType.IronOre;
        public int InputCount = 1;
        public ItemType OutputItem = ItemType.IronPlate;
        public int OutputCount = 1;
        public float ProcessingTime = 1f;

        public MachineRecipe ToMachineRecipe()
        {
            return new MachineRecipe
            {
                InputItem = InputItem,
                InputCount = Mathf.Max(1, InputCount),
                OutputItem = OutputItem,
                OutputCount = Mathf.Max(1, OutputCount),
                ProcessingTime = Mathf.Max(0.1f, ProcessingTime)
            };
        }
    }
}
