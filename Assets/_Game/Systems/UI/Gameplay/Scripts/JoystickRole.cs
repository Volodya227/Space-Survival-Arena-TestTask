using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI.Gameplay.Inputs
{
    internal sealed class JoystickRole
    {
        private readonly RectTransform _frame;
        private readonly RectTransform _handle;
        private readonly float _radius;
        private readonly UIInputs _inputs;
        private readonly Camera _uiCamera;

        private Vector2 _startLocalPoint;
        private bool _active;

        public JoystickRole(RectTransform frame, RectTransform handle, float radius, UIInputs inputs, Camera uiCamera)
        {
            _frame = frame;
            _handle = handle;
            _radius = radius;
            _inputs = inputs;
            _uiCamera = uiCamera;
        }

        public bool IsTarget(PointerEventData e)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                _frame, e.position, _uiCamera);
        }

        public void OnPointerDown(PointerEventData e)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _frame, e.position, _uiCamera, out _startLocalPoint))
                return;

            _active = true;
            _handle.anchoredPosition = Vector2.zero;
            _inputs.SignalMove(0f, 0f);
        }

        public void OnDrag(PointerEventData e)
        {
            if (!_active)
                return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _frame, e.position, _uiCamera, out var currentLocal))
                return;

            Vector2 delta = currentLocal - _startLocalPoint;
            Vector2 clamped = Vector2.ClampMagnitude(delta, _radius);

            _handle.anchoredPosition = clamped;

            _inputs.SignalMove(
                clamped.x / _radius,
                clamped.y / _radius
            );
        }

        public void OnPointerUp(PointerEventData e)
        {
            if (!_active)
                return;

            _active = false;
            _handle.anchoredPosition = Vector2.zero;
            _inputs.SignalMoveRelease();
        }
    }
}
