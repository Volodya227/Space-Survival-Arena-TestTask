using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
namespace Systems.Player.Inputs {
    public class PlayerInputCommandsOld : PlayerInputCommands
    {
        private int _pointerId = DefaultId;
        private const int DefaultId = int.MinValue;
        private Vector2 _position;
        private Vector2 _delta;
        public override int PointerId => _pointerId;
        public override bool PointerIdIsDefault => _pointerId == DefaultId;

        private bool _up;
        private bool _down;
        public override bool GetEscape => Input.GetKeyDown(KeyCode.Escape);
        public override float GetMoveX => Input.GetAxis("Horizontal");
        public override float GetMoveZ => Input.GetAxis("Vertical");
        public override bool GetMouseDown => Input.GetKeyDown(KeyCode.Mouse0) || _down;
        public override bool GetMouseUp => Input.GetKeyUp(KeyCode.Mouse0) || _up;
        public override float GetMoveMouseX => _pointerId != DefaultId ? _delta.x : Input.GetAxis("Mouse X");
        public override float GetMoveMouseY => _pointerId != DefaultId ? _delta.y : Input.GetAxis("Mouse Y");
        public override Vector3 MousePosition => _pointerId != DefaultId ? _position : Input.mousePosition;
        public override bool GetReloadPressed => Input.GetKeyDown(KeyCode.R);
        public override bool GetChangeViewKeeping => Input.GetKeyDown(KeyCode.V);//TODO delete "Down"
        public override bool GetChangeActiveUI => Input.GetKeyDown(KeyCode.U);
        public override bool GetChangeCharacter => Input.GetKeyDown(KeyCode.L);
        public override void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }
        public override void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }
        public override void Tick()
        {
            _down = false;
            _up = false;
            _delta = Vector2.zero;

            if (_pointerId == DefaultId)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    UnityEngine.Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (!_eventSystem.IsPointerOverGameObject(touch.fingerId))
                        {
                            _pointerId = touch.fingerId;
                            _position = touch.deltaPosition;
                            _delta = Vector2.zero;
                            _down = true;
                            break;
                        }
                    }
                }
                return;
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                UnityEngine.Touch touch = Input.GetTouch(i);
                if (touch.fingerId == _pointerId)
                {
                    _position = touch.position;
                    _delta = touch.deltaPosition;
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        _up = true;
                        _pointerId = DefaultId;
                    }
                    break;
                }
            }
        }
    }
}