using UnityEngine;
using AetherForge.Mobile;

namespace AetherForge.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float Reach = 5f;
        public WorldManager World;

        private Camera cam;
        private MobileInputManager mobileInput;

        private void Start()
        {
            cam = Camera.main;
            mobileInput = MobileInputManager.Instance;
        }

        private void Update()
        {
            bool isMobile = MobileInputManager.IsMobilePlatform();

            if (isMobile && mobileInput != null)
            {
                // Mobile input
                if (mobileInput.IsBreaking) TryBreakBlock();
                if (mobileInput.IsPlacing) TryPlaceBlock();
            }
            else
            {
                // Desktop input
                if (Input.GetMouseButtonDown(0)) TryBreakBlock();
                if (Input.GetMouseButtonDown(1)) TryPlaceBlock();
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
                World.SetBlock(pos, new VoxelData { Type = 3 });
            }
        }
    }
}