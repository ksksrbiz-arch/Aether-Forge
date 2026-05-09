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
        private const float DefaultJoystickScreenWidthRatio = 0.45f;
        private const float BreakZoneThreshold = 0.35f;
        private const float PlaceZoneThreshold = 0.65f;

        public static MobileInputManager Instance { get; private set; }

        [Header("Mobile Settings")]
        public float JoystickDeadzone = 0.1f;
        public float CameraSensitivity = 0.2f;
        public float MaxLookDelta = 6f;

        [Header("Virtual Joystick")]
        public RectTransform JoystickArea;
        public RectTransform JoystickHandle;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsBreaking { get; private set; }
        public bool IsPlacing { get; private set; }

        private Touchscreen touchscreen;
        private int joystickTouchId = -1;
        private bool breakButtonHeld;
        private bool placeButtonHeld;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            touchscreen = Touchscreen.current;
        }

        private void Update()
        {
            if (!IsMobilePlatform())
            {
                ResetInputs();
                return;
            }

            LookInput = Vector2.zero;
            MoveInput = Vector2.zero;
            bool breakFromZones = false;
            bool placeFromZones = false;
            bool joystickTouched = false;

            if (touchscreen == null)
            {
                IsBreaking = breakButtonHeld;
                IsPlacing = placeButtonHeld;
                return;
            }

            var touches = touchscreen.touches;
            for (int i = 0; i < touches.Count; i++)
            {
                var touch = touches[i];
                if (!touch.press.isPressed)
                    continue;

                var touchPos = touch.position.ReadValue();
                var touchId = touch.touchId.ReadValue();

                if (joystickTouchId == touchId || (joystickTouchId < 0 && IsInJoystickArea(touchPos)))
                {
                    joystickTouchId = touchId;
                    joystickTouched = true;
                    UpdateJoystickInput(touchPos);
                    continue;
                }

                var touchDelta = touch.delta.ReadValue();
                LookInput += touchDelta * CameraSensitivity;
                breakFromZones |= touchPos.y < Screen.height * BreakZoneThreshold;
                placeFromZones |= touchPos.y > Screen.height * PlaceZoneThreshold;
            }

            if (!joystickTouched)
            {
                joystickTouchId = -1;
                ResetJoystickHandle();
            }

            LookInput = Vector2.ClampMagnitude(LookInput, MaxLookDelta);
            IsBreaking = breakButtonHeld || breakFromZones;
            IsPlacing = placeButtonHeld || placeFromZones;
        }

        public static bool IsMobilePlatform()
        {
            return Application.isMobilePlatform || 
                   Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }

        public void SetBreakButtonState(bool isPressed)
        {
            breakButtonHeld = isPressed;
        }

        public void SetPlaceButtonState(bool isPressed)
        {
            placeButtonHeld = isPressed;
        }

        private bool IsInJoystickArea(Vector2 screenPosition)
        {
            if (JoystickArea == null)
                return screenPosition.x < Screen.width * DefaultJoystickScreenWidthRatio;

            return RectTransformUtility.RectangleContainsScreenPoint(JoystickArea, screenPosition, null);
        }

        private void UpdateJoystickInput(Vector2 screenPosition)
        {
            if (JoystickArea == null)
            {
                var normalized = new Vector2((screenPosition.x / Screen.width) * 2f - 1f, (screenPosition.y / Screen.height) * 2f - 1f);
                MoveInput = ApplyDeadzone(Vector2.ClampMagnitude(normalized, 1f));
                return;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickArea, screenPosition, null, out var localPoint);
            var radius = Mathf.Max(1f, Mathf.Min(JoystickArea.rect.width, JoystickArea.rect.height) * 0.5f);
            var normalizedPoint = Vector2.ClampMagnitude(localPoint / radius, 1f);
            MoveInput = ApplyDeadzone(normalizedPoint);

            if (JoystickHandle != null)
                JoystickHandle.anchoredPosition = normalizedPoint * radius;
        }

        private Vector2 ApplyDeadzone(Vector2 input)
        {
            if (input.sqrMagnitude < JoystickDeadzone * JoystickDeadzone)
                return Vector2.zero;

            return input;
        }

        private void ResetInputs()
        {
            MoveInput = Vector2.zero;
            LookInput = Vector2.zero;
            IsBreaking = false;
            IsPlacing = false;
            joystickTouchId = -1;
            ResetJoystickHandle();
        }

        private void ResetJoystickHandle()
        {
            if (JoystickHandle != null)
                JoystickHandle.anchoredPosition = Vector2.zero;
        }
    }
}
