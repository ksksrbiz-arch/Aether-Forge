using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.Kits
{
    /// <summary>
    /// Base ScriptableObject for all modular kits in AetherForge.
    /// Players unlock and place these to build factories quickly.
    /// </summary>
    [CreateAssetMenu(fileName = "NewKit", menuName = "AetherForge/Kit Definition")]
    public class KitDefinition : ScriptableObject
    {
        public string KitName;
        public string Description;
        public Sprite Icon;

        [Header("Contents")]
        public List<GameObject> Prefabs = new List<GameObject>();   // Machines, belts, etc.
        public List<Recipe> IncludedRecipes = new List<Recipe>();   // TODO: Define Recipe class

        [Header("Unlock Requirements")]
        public int ResearchCost;
        public List<string> RequiredTech = new List<string>();
    }
}