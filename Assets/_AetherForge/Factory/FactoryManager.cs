using UnityEngine;
using Unity.Entities;

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
}