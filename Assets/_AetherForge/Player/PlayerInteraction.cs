using UnityEngine;

namespace AetherForge.Player
{
    /// <summary>
    /// Handles player block breaking and placing.
    /// Attach to player camera or character.
    /// </summary>
    public class PlayerInteraction : MonoBehaviour
    {
        public float Reach = 5f;
        public WorldManager World;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Left click = break
            {
                TryBreakBlock();
            }

            if (Input.GetMouseButtonDown(1)) // Right click = place
            {
                TryPlaceBlock();
            }
        }

        private void TryBreakBlock()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Reach))
            {
                Vector3Int pos = Vector3Int.RoundToInt(hit.point - hit.normal * 0.5f);
                World.SetBlock(pos, VoxelData.Air);
            }
        }

        private void TryPlaceBlock()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Reach))
            {
                Vector3Int pos = Vector3Int.RoundToInt(hit.point + hit.normal * 0.5f);
                World.SetBlock(pos, new VoxelData { Type = 3 }); // Example: place "wood" or whatever
            }
            }
        }
    }
}