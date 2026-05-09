using UnityEngine;
using Unity.Entities;
using AetherForge.Kits;
using AetherForge.Factory;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance;
    
    private World world;
    
    private void Awake()
    {
        Instance = this;
        // Initialize DOTS world for factory
    }
    
    public void PlaceMachine(Vector3 position, MachineType type)
    {
        // Stub for placing machine in world
        Debug.Log($"Placed {type} at {position}");
    }

    public void PlaceStarterKit(StarterAutomationKitPlacer placer, Vector3 origin)
    {
        if (placer == null)
        {
            Debug.LogWarning("[AetherForge] Cannot place starter kit: placer is null.");
            return;
        }

        placer.PlaceStarterKit(origin);
        Debug.Log($"[AetherForge] Starter kit placed at {origin}");
    }
}
