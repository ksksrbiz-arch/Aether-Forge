using UnityEngine;
using System.Collections.Generic;
using AetherForge.Kits;

namespace AetherForge.Research
{
    /// <summary>
    /// Manages research points, unlocks, and the tech tree.
    /// Earn points from automation milestones.
    /// </summary>
    public class ResearchManager : MonoBehaviour
    {
        public static ResearchManager Instance { get; private set; }

        public int ResearchPoints { get; private set; } = 0;
        public List<ResearchNode> AllNodes = new List<ResearchNode>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddResearchPoints(int amount)
        {
            ResearchPoints += amount;
            Debug.Log($"+{amount} Research Points! Total: {ResearchPoints}");
        }

        public bool TryUnlockNode(ResearchNode node)
        {
            if (node.CanUnlock(ResearchPoints))
            {
                node.Unlock();
                ResearchPoints -= node.ResearchCost;

                // Example: Unlock new Kit when node is researched
                if (node.NodeName.Contains("Advanced Logistics"))
                {
                    // In real game: unlock new KitDefinition
                    Debug.Log("Advanced Logistics Kit Unlocked!");
                }

                return true;
            }
            return false;
        }

        // Call this from Assembler or other machines when they craft
        public void OnMachineCrafted()
        {
            AddResearchPoints(5); // Small reward per craft
        }
    }
}