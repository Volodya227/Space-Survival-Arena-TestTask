namespace Systems.Player.Inputs
{
    public class PlayerInputController : PlayerInput
    {
        private void Update()
        {
            if (_playerInputToUI.Active)
            {
                if (_playerInputCommands.GetEscape)
                {
                    _playerInputToUI.EventEscapeActivation();
                    SetActiveUIMenu();
                }
            }
            if (Active)
            {
                if (_playerInputCommands.GetChangeCharacter)
                {
                    ActivateEventChanageCharacter();
                }
                if (_playerInputCommands.GetChangeActiveUI)
                {
                    //Need use activate UI
                    SetActiveUIInput(!_isUI);// Get From Global Settings by event Change IGameplayDataGetter
                }
                if (_cameraViewInput.Active)
                {
                    
                    if (_dragMouse)
                    {                        
                        if (_playerInputCommands.GetMouseUp)
                            _dragMouse = false;
                    }
                    else
                    {
                        if (_playerInputCommands.GetMouseDown || !_isUI)
                        {
                            if (!_eventSystem.IsPointerOverGameObject())
                                _dragMouse = true;
                        }
                    }
                    if (_dragMouse)
                        SetCameraViewInput(_playerInputCommands.GetMoveMouseX, _playerInputCommands.GetMoveMouseY);
                    if (_playerInputCommands.GetChangeViewKeeping)
                    {
                        _cameraViewInput.ActivateChangeView();
                        //TODO add index
                    }
                }
                if (_weaponInput.Active)
                {
                    if (!_isUI)
                    {
                        if (_playerInputCommands.GetMouseDown)
                            ActivateEventAttackPressed();
                        if (_playerInputCommands.GetMouseUp)
                            ActivateEventAttackReleased();
                    }
                    if (_playerInputCommands.GetReloadPressed)
                        ActivateEventReload();
                }
            }
        }
        private void FixedUpdate()
        {
            if (Active) {
                if (_characterInput.Active)
                {
                    _characterInput.SetMoving(_playerInputCommands.GetMoveX + _inputUI.MoveX, _playerInputCommands.GetMoveZ + _inputUI.MoveZ);
                }
            }
        }
    }
}