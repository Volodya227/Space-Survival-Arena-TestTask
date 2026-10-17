using UnityEngine;
using UnityEngine.EventSystems;
namespace Systems.UI.Gameplay.Inputs
{
    public sealed class ButtonRole
    {
        public event System.Action EventDown;
        public event System.Action EventUp;
        private int _pointerId = DefaultId;
        private const int DefaultId = int.MinValue;
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
            if (_pointerId == DefaultId)
            {
                _pointerId = e.pointerId;
                EventDown?.Invoke();
            }
        }
        public void OnPointerUp(PointerEventData e)
        {
            if (e.pointerId == _pointerId)
                Cancel();
        }
        public void Cancel()
        {
            if (_pointerId == DefaultId)
                return;
            _pointerId = DefaultId;
            EventUp?.Invoke();
        }
    }
}