namespace Systems.Player.Inputs
{
    public abstract class PlayerInput : UnityEngine.MonoBehaviour
    {
        public event System.Action EventChanageCharacter;
        protected bool _isUI = false;
        private bool _bindedUIInput = false;
        protected readonly PlayerCharacterInput _characterInput = new();
        protected readonly PlayerCameraViewInput _cameraViewInput = new();
        protected readonly PlayerWeaponInput _weaponInput = new();
        protected readonly PlayerInputToUI _playerInputToUI = new();
        protected UI.Gameplay.Inputs.IUIInputs _inputUI;
        private readonly UI.Gameplay.Inputs.UIInputsNullReference _inputUINullRef = new();
        public PlayerCharacterInput GetCharacterInput => _characterInput;
        public PlayerCameraViewInput CameraViewInput => _cameraViewInput;
        public PlayerWeaponInput GetWeaponInput => _weaponInput;
        public PlayerInputToUI InputToUI => _playerInputToUI;
        public bool Active { get; private set; } = false;
        protected UnityEngine.EventSystems.EventSystem _eventSystem;
        protected bool _dragMouse;
        protected void Awake()
        {
            SetUI(null);
            _cameraViewInput.EventChangeCameraView += SetViewToCharacter;
            _playerInputToUI.EventSetActiveUI += SetActiveUIMenu;
            SetActiveUIInput(true);//correctly set state
        }
        private void OnDestroy()
        {
            _playerInputToUI.EventSetActiveUI -= SetActiveUIMenu;
            _cameraViewInput.EventChangeCameraView -= SetViewToCharacter;
        }
        public void SetEventSystem(UnityEngine.EventSystems.EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }
        public void SetActiveUIInput(bool value)
        {
            _isUI = value;
            _cameraViewInput.SetMouseLockMode(!_isUI);
            if (_bindedUIInput)
                UnbindUI();
            if (_isUI)
                BindUI();
            _inputUI?.SetGameplayMode(_isUI);
            ResetInput();
        }
        private void ResetInput()
        {
            _dragMouse = false;
            _weaponInput?.InputAttackReleased();
            _inputUI.Reset();
            SetViewToCharacter();
        }
        public void Dispose()
        {
            _eventSystem = null;
            SetActiveUIInput(false);
        }
        private void SetViewToCharacter()
        {
            _characterInput.SetView(_cameraViewInput.CameraRotationX, _cameraViewInput.CameraRotationY);
        }
        public void SetActive(bool value)
        {
            Active = value;
            if(Active)
                _cameraViewInput.SetMouseLockMode(!_isUI);
            else
                _cameraViewInput.SetMouseLockMode(false);
        }
        public void SetUI(UI.Gameplay.Inputs.IUIInputs inputUI)
        {
            UnbindUI();
            _inputUI = inputUI ?? _inputUINullRef;
            SetActiveUIInput(_isUI);
        }
        protected void SetCameraViewInput(float x, float y)
        {
            _cameraViewInput.SetXY(x, y);
        }
        protected void ActivateEventChanageCharacter()
        {
            EventChanageCharacter?.Invoke();
        }
        //TODO bind event from UI
        private void BindUI()
        {
            if (!_isUI) return;
            _bindedUIInput = true;
            if (_inputUI == null) return;
            _inputUI.EventAttackPressed += ActivateEventAttackPressed;
            _inputUI.EventAttackReleased += ActivateEventAttackReleased;
            _inputUI.EventReloading += ActivateEventReload;
            _inputUI.EventOpenMenu += OpenMenu;
        }
        private void UnbindUI()
        {
            _bindedUIInput = false;
            if (_inputUI == null) return;
            _inputUI.EventAttackPressed -= ActivateEventAttackPressed;
            _inputUI.EventAttackReleased -= ActivateEventAttackReleased;
            _inputUI.EventReloading -= ActivateEventReload;
            _inputUI.EventOpenMenu -= OpenMenu;
        }
        protected void ActivateEventAttackPressed()
        {
            if (_weaponInput.Active)
                _weaponInput.InputAttackPressed();
        }
        protected void ActivateEventAttackReleased()
        {
            if (_weaponInput.Active)
                _weaponInput.InputAttackReleased();
        }
        protected void ActivateEventReload()
        {
            if (_weaponInput.Active)
                _weaponInput.InputReload();
        }
        protected void SetActiveUIMenu()
        {
            if (_playerInputToUI.EventAskActiveUIActivation())
            {
                //ChangeCameraDrag();
                SetActive(false);
            }
            else
            {
                SetActive(true);
            }
        }
        protected void OpenMenu()
        {
            _playerInputToUI.EventEscapeActivation();
            SetActiveUIMenu();
        }
    }
}