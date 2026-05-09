using UnityEngine;
using UnityEngine.InputSystem;

namespace AetherForge.Mobile
{
    /// <summary>
    /// Handles all mobile touch input with virtual joystick support.
    /// Full movement + break/place for mobile playability.
    /// </summary>
    public class MobileInputManager : MonoBehaviour
    {
        public static MobileInputManager Instance { get; private set; }

        [Header("Mobile Settings")]
        public float JoystickDeadzone = 0.15f;
        public float JoystickSensitivity = 1.5f;

        public Vector2 MoveInput { get; private set; }
        public bool IsBreaking { get; private set; }
        public bool IsPlacing { get; private set; }

        private Touchscreen touchscreen;
        private Vector2 joystickStartPos;
        private bool isDraggingJoystick;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            touchscreen = Touchscreen.current;
        }

        private void Update()
        {
            if (!IsMobilePlatform() || touchscreen == null) return;

            var primaryTouch = touchscreen.primaryTouch;

            if (primaryTouch.isInProgress)
            {
                Vector2 touchPos = primaryTouch.position.ReadValue();

                if (touchPos.x < Screen.width * 0.4f)
                {
                    if (!isDraggingJoystick)
                    {
                        joystickStartPos = touchPos;
                        isDraggingJoystick = true;
                    }

                    Vector2 delta = (touchPos - joystickStartPos) / (Screen.width * 0.2f);
                    MoveInput = Vector2.ClampMagnitude(delta * JoystickSensitivity, 1f);

                    if (MoveInput.magnitude < JoystickDeadzone) MoveInput = Vector2.zero;
                }
                else
                {
                    isDraggingJoystick = false;
                    MoveInput = Vector2.zero;

                    IsBreaking = touchPos.y < Screen.height * 0.5f;
                    IsPlacing = touchPos.y >= Screen.height * 0.5f;
                }
            }
            else
            {
                isDraggingJoystick = false;
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