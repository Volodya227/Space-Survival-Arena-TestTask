using UnityEngine;
using UnityEngine.EventSystems;
namespace Systems.UI.Gameplay.Inputs
{
    public sealed class JoystickRole
    {
        public event System.Action<float, float> EventInput;
        private int _pointerId = -1;
        public int PointerId => _pointerId;
        private readonly RectTransform _frame;
        private readonly RectTransform _handle;
        private readonly float _radius;
        private readonly Camera _uiCamera;
        private readonly bool _useX;
        private readonly bool _useY;
        private readonly bool _useReset;
        private Vector2 _startLocalPoint;
        public JoystickRole(RectTransform frame, RectTransform handle, float radius, Camera uiCamera, bool useX = true, bool useY = true, bool useReset = true)
        {
            _frame = frame;
            _handle = handle;
            _radius = radius;
            _uiCamera = uiCamera;
            _useX = useX;
            _useY = useY;
            _useReset = useReset;
        }
        public bool IsTarget(PointerEventData e)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_frame, e.position, _uiCamera);
        }
        public void OnPointerDown(PointerEventData e)
        {
            if (_pointerId >= 0)
                return;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_frame, e.position, _uiCamera, out var localPoint))
                return;

            _pointerId = e.pointerId;
            Debug.Log("Poiner Down" + _pointerId);
            if (_useReset)
            {
                _handle.anchoredPosition = Vector2.zero;
                EventInput?.Invoke(0, 0);
                _startLocalPoint = localPoint;
            }
            else
            {
                _startLocalPoint = localPoint - _handle.anchoredPosition;
            }
        }
        public void OnDrag(PointerEventData e)
        {
            if (e.pointerId != _pointerId)
                return;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_frame, e.position, _uiCamera, out var currentLocal))
                return;
            Vector2 delta = currentLocal - _startLocalPoint;
            if (!_useX)
                delta.x = 0;
            if (!_useY)
                delta.y = 0;
            Vector2 clamped = Vector2.ClampMagnitude(delta, _radius);
            _handle.anchoredPosition = clamped;
            EventInput?.Invoke(clamped.x / _radius, clamped.y / _radius);
        }
        public void OnPointerUp(PointerEventData e)
        {
            if (e.pointerId == _pointerId)
                Release();
        }
        private void Release()
        {
            Debug.Log("Poiner Up" + _pointerId);
            _pointerId = -1;
            if (_useReset)
            {
                _handle.anchoredPosition = Vector2.zero;
                EventInput?.Invoke(0, 0);
            }
        }
        public void Cancel()
        {
            Release();
            _handle.anchoredPosition = Vector2.zero;
            EventInput?.Invoke(0, 0);
        }
    }
}
