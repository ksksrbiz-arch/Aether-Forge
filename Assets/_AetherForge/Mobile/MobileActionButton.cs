using UnityEngine;
using UnityEngine.EventSystems;

namespace AetherForge.Mobile
{
    public class MobileActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public enum ActionType
        {
            Break,
            Place
        }

        public ActionType Action;

        public void OnPointerDown(PointerEventData eventData)
        {
            SetState(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetState(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetState(false);
        }

        private void SetState(bool isPressed)
        {
            if (MobileInputManager.Instance == null)
                return;

            if (Action == ActionType.Break)
                MobileInputManager.Instance.SetBreakButtonState(isPressed);
            else
                MobileInputManager.Instance.SetPlaceButtonState(isPressed);
        }
    }
}
