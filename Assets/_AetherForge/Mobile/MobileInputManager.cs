using UnityEngine;
using UnityEngine.InputSystem;

namespace AetherForge.Mobile
{
    /// <summary>
    /// Handles all mobile touch input.
    /// Supports virtual joystick + action buttons.
    /// Works alongside desktop input.
    /// </summary>
    public class MobileInputManager : MonoBehaviour
    {
        public static MobileInputManager Instance { get; private set; }

        [Header("Mobile Settings")]
        public float JoystickDeadzone = 0.1f;

        public Vector2 MoveInput { get; private set; }
        public bool IsBreaking { get; private set; }
        public bool IsPlacing { get; private set; }

        private Touchscreen touchscreen;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            touchscreen = Touchscreen.current;
        }

        private void Update()
        {
            if (!IsMobilePlatform()) return;

            // Simple touch handling (expand with UI joystick later)
            if (touchscreen != null && touchscreen.primaryTouch.isInProgress)
            {
                Vector2 touchPos = touchscreen.primaryTouch.position.ReadValue();
                // TODO: Map to virtual joystick area
                MoveInput = Vector2.zero; // Placeholder

                // Simulate break/place with touch zones
                IsBreaking = touchPos.y < Screen.height * 0.3f;
                IsPlacing = touchPos.y > Screen.height * 0.7f;
            }
            else
            {
                MoveInput = Vector2.zero;
                IsBreaking = false;
                IsPlacing = false;
            }
        }

        public static bool IsMobilePlatform()
        {
            return Application.isMobilePlatform || 
                   Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }
}