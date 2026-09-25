namespace Systems.Player.Inputs
{
    public abstract class PlayerInput : UnityEngine.MonoBehaviour
    {
        public event System.Action EventChanageCharacter;
        protected PlayerInputCommands _playerInputCommands;
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
        private UI.Gameplay.IUIState _stateUI;
        protected void Awake()
        {
            _playerInputCommands = new PlayerInputCommandsOld();
            SetUIInput(null);
            _cameraViewInput.EventChangeCameraView += SetViewToCharacter;
            SetActiveUIInput(false);//correctly set state
        }
        private void OnEnable()
        {
            _playerInputCommands.OnEnable();
        }
        private void OnDisable()
        {
            _playerInputCommands.OnDisable();
        }
        private void OnDestroy()
        {
            _cameraViewInput.EventChangeCameraView -= SetViewToCharacter;
            SetUIInput(null, true);
            SetUI();
            _playerInputCommands.Dispose();
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
                UnbindUIInput();
            if (_isUI)
            {
                BindUIInput();
                _inputUI.SetGameplayMode(_isUI);
                ResetInput();
            }
        }
        private void ResetInput()
        {
            _dragMouse = false;
            _weaponInput?.InputAttackReleased();
            _inputUI?.Reset();
            SetViewToCharacter();
        }
        public void Dispose()
        {
            _eventSystem = null;
            SetActiveUIInput(false);
            SetUIInput(null, true);//input can use between scenes
            SetUI();
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
        public void SetUIInput(UI.Gameplay.Inputs.IUIInputs inputUI, bool lose = false)
        {
            UnbindUIInput();
            _inputUI = inputUI ?? _inputUINullRef;
            if (lose)
                _inputUI = null;
            SetActiveUIInput(_isUI);
        }
        public void SetUI(UI.Gameplay.IUIState stateUI = null)
        {
            if (_stateUI != null)
                UnbindUI();
            _stateUI = stateUI;
            if (_stateUI != null)
                BindUI();
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
        private void BindUIInput()
        {
            if (!_isUI) return;
            if (_inputUI == null) return;
            _bindedUIInput = true;
            _inputUI.EventAttackPressed += ActivateEventAttackPressed;
            _inputUI.EventAttackReleased += ActivateEventAttackReleased;
            _inputUI.EventReloading += ActivateEventReload;
            _inputUI.EventOpenMenu += OpenMenu;
        }
        private void BindUI()
        {
            _stateUI.EventChangeActiveUI += UpdateInputStateByUI;
        }
        private void UnbindUIInput()
        {
            if (_inputUI == null) return;
            _bindedUIInput = false;
            _inputUI.EventAttackPressed -= ActivateEventAttackPressed;
            _inputUI.EventAttackReleased -= ActivateEventAttackReleased;
            _inputUI.EventReloading -= ActivateEventReload;
            _inputUI.EventOpenMenu -= OpenMenu;
        }
        private void UnbindUI()
        {
            _stateUI.EventChangeActiveUI -= UpdateInputStateByUI;
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
            _playerInputToUI.EventAskActiveUIActivation();
        }
        private void UpdateInputStateByUI()
        {
            if(_stateUI.ActiveUI)
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