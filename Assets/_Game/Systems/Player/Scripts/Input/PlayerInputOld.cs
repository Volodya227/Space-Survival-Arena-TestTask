using UnityEngine;
namespace Systems.Player.Inputs
{
    public class PlayerInputOld : PlayerInput
    {
        private void Update()
        {
            if (_playerInputToUI.Active)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _playerInputToUI.EventEscapeActivation();
                    SetActiveUIMenu();
                }
            }
            if (Active)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    ActivateEventChanageCharacter();
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    SetActiveUIInput(!_isUI);
                }
                if (_cameraViewInput.Active)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) || !_isUI)
                    {
                        if(!_eventSystem.IsPointerOverGameObject())
                            _dragMouse = true;
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                        _dragMouse = false;
                    if (_dragMouse)
                        SetCameraViewInput(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        _cameraViewInput.ActivateChangeView();
                        //TODO
                    }
                }
                if (_weaponInput.Active)
                {
                    if (!_isUI)
                    {
                        if (Input.GetMouseButtonDown(0))
                            ActivateEventAttackPressed();
                        if (Input.GetMouseButtonUp(0))
                            ActivateEventAttackReleased();
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                        ActivateEventReload();
                }
            }
        }
        private void FixedUpdate()
        {
            if (Active) {
                if (_characterInput.Active)
                {
                    _characterInput.SetMoving(Input.GetAxis("Horizontal") + _inputUI.MoveX, Input.GetAxis("Vertical") + _inputUI.MoveZ);
                }
            }
        }
    }
}