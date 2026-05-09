using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using AetherForge.Research;

namespace AetherForge.UI
{
    /// <summary>
    /// Simple Research Tree UI.
    /// Displays available nodes, cost, prerequisites, and unlock buttons.
    /// </summary>
    public class ResearchUI : MonoBehaviour
    {
        public ResearchManager researchManager;
        public Transform nodeContainer; // UI parent for buttons
        public GameObject nodeButtonPrefab; // Button prefab with Text + Button

        private List<Button> activeButtons = new List<Button>();

        private void Start()
        {
            if (researchManager == null)
                researchManager = FindObjectOfType<ResearchManager>();

            RefreshUI();
        }

        public void RefreshUI()
        {
            // Clear old buttons
            foreach (var btn in activeButtons) Destroy(btn.gameObject);
            activeButtons.Clear();

            foreach (var node in researchManager.AllNodes)
            {
                if (node.IsUnlocked) continue; // Hide unlocked for now

                GameObject btnObj = Instantiate(nodeButtonPrefab, nodeContainer);
                Button btn = btnObj.GetComponent<Button>();
                Text txt = btnObj.GetComponentInChildren<Text>();

                txt.text = $"{node.NodeName}\nCost: {node.ResearchCost}\n{node.Description}";

                bool canUnlock = node.CanUnlock(researchManager.ResearchPoints);
                btn.interactable = canUnlock;

                btn.onClick.AddListener(() =>
                {
                    if (researchManager.TryUnlockNode(node))
                    {
                        RefreshUI(); // Refresh after unlock
                    }
                });

                activeButtons.Add(btn);
            }
        }

        // Call this from a button or key to open/close the research panel
        public void ToggleUI()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            if (gameObject.activeSelf) RefreshUI();
        }
    }
}