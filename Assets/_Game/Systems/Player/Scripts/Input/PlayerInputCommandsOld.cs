using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
namespace Systems.Player.Inputs {
    public class PlayerInputCommandsOld : PlayerInputCommands
    {
        private int _pointerId = -1;
        private Vector2 _position;
        private Vector2 _delta;
        public override int PointerId => _pointerId;

        private bool _up;
        private bool _down;

        public override bool GetEscape => Input.GetKeyDown(KeyCode.Escape);
        public override float GetMoveX => Input.GetAxis("Horizontal");
        public override float GetMoveZ => Input.GetAxis("Vertical");
        public override bool GetMouseDown => Input.GetKeyDown(KeyCode.Mouse0) || _down;
        public override bool GetMouseUp => Input.GetKeyUp(KeyCode.Mouse0) || _up;
        public override float GetMoveMouseX => _pointerId >= 0 ? _delta.x : Input.GetAxis("Mouse X");
        public override float GetMoveMouseY => _pointerId >= 0 ? _delta.y : Input.GetAxis("Mouse Y");
        public override Vector3 MousePosition => _pointerId >= 0 ? _position : Input.mousePosition;
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

            if (_pointerId < 0)
            {
                foreach (var touch in Touch.activeTouches)
                {
                    if (touch.phase != TouchPhase.Began)
                        continue;
                    if (_eventSystem.IsPointerOverGameObject(touch.touchId))
                        continue;
                    _pointerId = touch.touchId;
                    _position = touch.screenPosition;
                    _delta = Vector2.zero;
                    _down = true;
                    break;
                }
                return;
            }
            foreach (var touch in Touch.activeTouches)
            {
                if (touch.touchId != _pointerId)
                    continue;
                _position = touch.screenPosition;
                _delta = touch.delta;
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _up = true;
                    _pointerId = -1;
                }
                break;
            }
        }
    }
}