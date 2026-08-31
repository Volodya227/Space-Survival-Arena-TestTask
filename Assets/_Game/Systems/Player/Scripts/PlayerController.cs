namespace Systems.Player
{
    public class PlayerController
    {
        private readonly Inputs.PlayerInput _input;
        private readonly UI.Gameplay.UISystem _systemUI;
        private Character.CharacterController _character;
        private readonly CameraView.CameraView _cameraView;
        public PlayerController(Inputs.PlayerInput input, CameraView.CameraView cameraView, UI.Gameplay.UISystem systemUI)
        {
            _input = input;
            _cameraView = cameraView;
            _cameraView.SetInput(_input.CameraViewInput);
            _systemUI = systemUI;
            _systemUI.SetInput(_input.InputToUI);
        }
        public void Dispose()
        {
        }
        public void SetCharacter(Character.CharacterController character)
        {
            _systemUI.GetCharacterUI.SetState(null);
            _systemUI.GetWeaponUI.SetState(null);

            _character = character;
            if (_character == null) {
                return;
            }
            _character.SetInput(_input.GetCharacterInput, _input.GetWeaponInput);
            ChangedStateHuman();
            //UI
            _systemUI.GetCharacterUI.SetState(_character.State);
            _systemUI.GetWeaponUI.SetState(_character.State.WeaponContainerData);
            //TODO unbind Weapon from UI when character get or lose weapon
            //now have not this logic because weapon is item character without switch
        }
        private void ChangedStateHuman()
        {
            CameraView.ViewData view = _cameraView.GetFirstViewData;
            view.parent = _character.FirstView;
            view.minDist = 0;
            view.localView = false;
            view = _cameraView.GetThirdViewData;
            view.parent = _character.ThirdView;
            view.minDist = -8;//TODO
            view = _cameraView.GetTopDownViewData;
            view.parent = _character.ThirdView;
            view.minDist = -40;//TODO
            _cameraView.UpdateView(true);
        }
    }
}