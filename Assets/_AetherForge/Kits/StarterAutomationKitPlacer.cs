using UnityEngine;

namespace AetherForge.Kits
{
    public class StarterAutomationKitPlacer : MonoBehaviour
    {
        public KitDefinition StarterAutomationKit;
        public Transform PlacementRoot;

        public void PlaceStarterKit(Vector3 worldOrigin)
        {
            if (StarterAutomationKit == null)
            {
                Debug.LogWarning("[AetherForge] Starter Automation Kit is not assigned.");
                return;
            }

            var root = PlacementRoot == null ? null : PlacementRoot;
            for (int i = 0; i < StarterAutomationKit.Placements.Count; i++)
            {
                var placement = StarterAutomationKit.Placements[i];
                if (placement.Prefab == null)
                    continue;

                var rotation = Quaternion.Euler(placement.LocalEulerAngles);
                var worldPosition = worldOrigin + placement.LocalPosition;
                Instantiate(placement.Prefab, worldPosition, rotation, root);
            }
        }
    }
}
