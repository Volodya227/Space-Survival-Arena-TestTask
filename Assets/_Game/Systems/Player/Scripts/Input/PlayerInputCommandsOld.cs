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
        public override float GetMoveMouseX => Input.touchCount > 0 ? _delta.x : Input.GetAxis("Mouse X");
        public override float GetMoveMouseY => Input.touchCount > 0 ? _delta.y : Input.GetAxis("Mouse Y");
        public override Vector3 MousePosition => Input.touchCount > 0 ? _position : Input.mousePosition;
        public override bool GetReloadPressed => Input.GetKeyDown(KeyCode.R);
        public override bool GetChangeViewKeeping => Input.GetKeyDown(KeyCode.V);//TODO delete "Down"
        public override bool GetChangeActiveUI => Input.GetKeyDown(KeyCode.U);
        public override bool GetChangeCharacter => Input.GetKeyDown(KeyCode.L);
        public override void OnEnable()
        {
            Input.simulateMouseWithTouches = false;
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
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        if (!_eventSystem.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                        {
                            _pointerId = Input.GetTouch(i).fingerId;
                            _position = Input.GetTouch(i).position;
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
                if (Input.GetTouch(i).fingerId == _pointerId)
                {
                    _position = Input.GetTouch(i).position;
                    _delta = Input.GetTouch(i).deltaPosition;
                    if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
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