using UnityEngine;
using UnityEngine.EventSystems;
namespace Systems.UI.Gameplay.Inputs
{
    internal sealed class FireButtonRole
    {
        private readonly RectTransform _button;
        private readonly UIInputs _inputs;
        private readonly Camera _uiCamera;
        private bool _pressed;
        public FireButtonRole(RectTransform button, UIInputs inputs, Camera uiCamera)
        {
            _button = button;
            _inputs = inputs;
            _uiCamera = uiCamera;
        }

        public bool IsTarget(PointerEventData e)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_button, e.position, _uiCamera);
        }
        public void OnPointerDown(PointerEventData e)
        {
            if (_pressed)
                return;
            _pressed = true;
            _inputs.SignalAttackPressed();
        }
        public void OnPointerUp(PointerEventData e)
        {
            if (!_pressed)
                return;
            _pressed = false;
            _inputs.SignalAttackReleased();
        }
    }
    internal sealed class ReloadButtonRole
    {
        private readonly RectTransform _button;
        private readonly UIInputs _inputs;
        private readonly Camera _uiCamera;
        public ReloadButtonRole(RectTransform button, UIInputs inputs, Camera uiCamera)
        {
            _button = button;
            _inputs = inputs;
            _uiCamera = uiCamera;
        }
        public bool IsTarget(PointerEventData e)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                _button, e.position, _uiCamera);
        }
        public void OnPointerDown(PointerEventData e)
        {
            _inputs.SignalReloading();
        }
    }
}

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