using UnityEngine;
using AetherForge.Mobile;
using AetherForge.World;

namespace AetherForge.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float Reach = 5f;
        public float MoveSpeed = 5f;
        public float LookSensitivity = 2f;
        public float DesktopLookMultiplier = 3f;
        public float PitchClamp = 80f;
        public WorldManager World;
        public Transform PlayerRoot;

        private Camera cam;
        private MobileInputManager mobileInput;
        private float pitch;

        private void Start()
        {
            cam = Camera.main;
            mobileInput = MobileInputManager.Instance;
            if (PlayerRoot == null) PlayerRoot = transform;

            if (cam != null)
                pitch = cam.transform.localEulerAngles.x;
        }

        private void Update()
        {
            bool isMobile = MobileInputManager.IsMobilePlatform();
            Vector2 moveInput = Vector2.zero;
            Vector2 lookInput = Vector2.zero;

            if (isMobile && mobileInput != null)
            {
                moveInput = mobileInput.MoveInput;
                lookInput = mobileInput.LookInput;
                if (mobileInput.IsBreaking) TryBreakBlock();
                if (mobileInput.IsPlacing) TryPlaceBlock();
            }
            else
            {
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * DesktopLookMultiplier;
                if (Input.GetMouseButtonDown(0)) TryBreakBlock();
                if (Input.GetMouseButtonDown(1)) TryPlaceBlock();
            }

            ApplyLook(lookInput);
            ApplyMovement(moveInput);
        }

        private void TryBreakBlock()
        {
            if (cam == null || World == null) return;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Reach))
            {
                Vector3Int pos = Vector3Int.RoundToInt(hit.point - hit.normal * 0.5f);
                World.SetBlock(pos, VoxelData.Air);
            }
        }

        private void TryPlaceBlock()
        {
            if (cam == null || World == null) return;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Reach))
            {
                Vector3Int pos = Vector3Int.RoundToInt(hit.point + hit.normal * 0.5f);
                World.SetBlock(pos, new VoxelData { Type = 3 });
            }
        }

        private void ApplyMovement(Vector2 moveInput)
        {
            if (PlayerRoot == null) return;

            var forward = PlayerRoot.forward;
            forward.y = 0f;
            forward.Normalize();

            var right = PlayerRoot.right;
            right.y = 0f;
            right.Normalize();

            var direction = (forward * moveInput.y + right * moveInput.x);
            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            PlayerRoot.position += direction * (MoveSpeed * Time.deltaTime);
        }

        private void ApplyLook(Vector2 lookInput)
        {
            if (PlayerRoot == null || cam == null) return;

            PlayerRoot.Rotate(Vector3.up, lookInput.x * LookSensitivity, Space.World);
            pitch -= lookInput.y * LookSensitivity;
            pitch = Mathf.Clamp(pitch, -PitchClamp, PitchClamp);
            cam.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }
    }
}
