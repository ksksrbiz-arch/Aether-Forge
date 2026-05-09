using UnityEngine;
using System;

namespace AetherForge.Research
{
    [CreateAssetMenu(fileName = "NewResearchNode", menuName = "AetherForge/Research Node")]
    public class ResearchNode : ScriptableObject
    {
        public string NodeName;
        public string Description;
        public int ResearchCost;
        public ResearchNode[] Prerequisites; // Nodes that must be unlocked first

        public bool IsUnlocked { get; private set; }

        public void Unlock()
        {
            IsUnlocked = true;
            Debug.Log($"Unlocked: {NodeName}");
        }

        public bool CanUnlock(int currentPoints)
        {
            if (IsUnlocked) return false;
            if (currentPoints < ResearchCost) return false;

            foreach (var prereq in Prerequisites)
            {
                if (prereq != null && !prereq.IsUnlocked) return false;
            }
            return true;
        }
    }
}