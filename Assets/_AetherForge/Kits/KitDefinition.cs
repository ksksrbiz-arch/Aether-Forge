using UnityEngine;
using System.Collections.Generic;
using AetherForge.Factory;

namespace AetherForge.Kits
{
    /// <summary>
    /// Base ScriptableObject for all modular kits in AetherForge.
    /// Players unlock and place these to build factories quickly.
    /// </summary>
    [CreateAssetMenu(fileName = "NewKit", menuName = "AetherForge/Kit Definition")]
    public class KitDefinition : ScriptableObject
    {
        [System.Serializable]
        public struct KitPlacement
        {
            public GameObject Prefab;
            public Vector3 LocalPosition;
            public Vector3 LocalEulerAngles;
        }

        public string KitName;
        public string Description;
        public Sprite Icon;

        [Header("Contents")]
        public List<GameObject> Prefabs = new List<GameObject>();
        public List<KitPlacement> Placements = new List<KitPlacement>();
        public List<RecipeDefinition> IncludedRecipes = new List<RecipeDefinition>();

        [Header("Unlock Requirements")]
        public int ResearchCost;
        public List<string> RequiredTech = new List<string>();
    }
}
