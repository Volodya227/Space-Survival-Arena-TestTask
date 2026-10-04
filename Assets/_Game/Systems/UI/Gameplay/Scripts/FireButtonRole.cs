using UnityEngine;
using UnityEngine.EventSystems;
namespace Systems.UI.Gameplay.Inputs
{
    public sealed class ButtonRole
    {
        public event System.Action EventDown;
        public event System.Action EventUp;
        private int _pointerId = -1;
        private readonly RectTransform _button;
        private readonly Camera _uiCamera;
        public ButtonRole(RectTransform button, Camera uiCamera)
        {
            _button = button;
            _uiCamera = uiCamera;
        }
        public bool IsTarget(PointerEventData e)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_button, e.position, _uiCamera);
        }
        public void OnPointerDown(PointerEventData e)
        {
            if (_pointerId >= 0)
                return;
            _pointerId = e.pointerId;
            EventDown?.Invoke();
        }
        public void OnPointerUp(PointerEventData e)
        {
            if (e.pointerId != _pointerId)
                return;
            _pointerId = -1;
            EventUp?.Invoke();
        }
        public void Cancel()
        {
            if (_pointerId < 0)
                return;
            _pointerId = -1;
            EventUp?.Invoke();
        }
    }
}