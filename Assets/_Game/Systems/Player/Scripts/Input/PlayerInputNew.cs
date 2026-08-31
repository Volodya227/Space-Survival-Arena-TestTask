/*using UnityEngine;
namespace Player.Inputs
{
    public class PlayerInputNew : PlayerInput
    {
        private NewInputConfig _actions;
        protected new void Awake()
        {
            base.Awake();
            _actions = new NewInputConfig();
        }
        private void OnEnable()
        {
            _actions.Enable();
        }
        private void OnDisable()
        {
            _actions.Disable();
        }
        private void Update()
        {
            if (Active)
            {
                if (_actions.Player.ChangeCharacter.triggered)
                    ActivateEventChanageCharacter();
                if (_cameraViewInput.Active)
                {
                    if (!_isUI)
                    {
                        Vector2 look = _actions.Camera.Look.ReadValue<Vector2>();
                        SetCameraViewInput(look.x, look.y);
                    }

                    if (_actions.Camera.ChangeView.triggered)
                        _cameraViewInput.ActivateChangeView();
                }
                if (_weaponInput.Active)
                {
                    if (!_isUI)
                    {
                        var attack = _actions.Weapon.Attack;

                        if (attack.WasPressedThisFrame())
                            _weaponInput.InputAttackPressed();

                        if (attack.WasReleasedThisFrame())
                            _weaponInput.InputAttackReleased();
                    }

                    if (_actions.Weapon.Reloading.triggered)
                        _weaponInput.InputReload();
                }
            }
        }
        private void FixedUpdate()
        {
            if (Active)
            {
                if (_characterInput.Active)
                {
                    Vector2 move = _actions.Character.Move.ReadValue<Vector2>();
                    _characterInput.SetMoving(move.x + _inputUI.MoveX, move.y + _inputUI.MoveZ);
                }
            }
        }
    }
}//*/