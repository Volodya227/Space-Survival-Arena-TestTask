using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay
{
    public abstract class UIMenuBase : IMenuEvents
    {
        public event System.Action EventExitScene;
        protected bool _active = false;
        public bool Active => _active;
        [SerializeField] protected GameObject _panel;
        [SerializeField] protected Button _settings;
        [SerializeField] protected Button _returnMainMenu;
        [SerializeField] protected Settings.UISettingsSystem _settingsSystemPrefab;
        protected Settings.UISettingsSystem _settingsSystem = null;
        protected Data.ApplicationData.IApplicationData _applicationData = null;
        private UIState _stateUI;

        protected Inputs.InputToUI _input;
        protected abstract void InitLocal();
        protected abstract void DisposeLocal();
        public void Init(Data.ApplicationData.IApplicationData applicationData = null, UIState stateUI = null)
        {
            _stateUI = stateUI;
            HideMenu();
            _settings.onClick.AddListener(OpenSettings);
            _returnMainMenu.onClick.AddListener(Exit);
            _applicationData = applicationData;
            InitLocal();
        }
        public void Dispose()
        {
            _settings.onClick.RemoveListener(OpenSettings);
            _returnMainMenu.onClick.RemoveListener(Exit);
            if (_settingsSystem != null)
            {
                _settingsSystem.EventDisable -= ShowMenu;
            }
            _active = false;
            DisposeLocal();
        }
        private void Exit()
        {
            EventExitScene?.Invoke();
        }
        private void OpenSettings()
        {
            if (_settingsSystem == null)
            {
                _settingsSystem = Object.Instantiate(_settingsSystemPrefab);
                _settingsSystem.SetData(_applicationData);
                _settingsSystem.Init();
                _settingsSystem.EventDisable += ShowMenu;
                _settingsSystem.transform.parent = _panel.transform;
            }
            _settingsSystem.Open();
        }
        private void ShowMenu()
        {
            _active = true;
            _panel.SetActive(true);
        }
        private void HideMenu()
        {
            _active = false;
            _panel.SetActive(false);
        }
        public void SetInput(Inputs.InputToUI input)
        {
            if (_input != null)
            {
                UnbindInput();
            }
            _input = input;
            if (_input != null)
            {
                BindInput();
            }
        }
        protected virtual void BindInput() { }
        protected virtual void UnbindInput() { }
        protected bool ChangeActiveBase(bool onlyHide = false)
        {
            if (_active)
            {
                if (_settingsSystem != null)
                {
                    if (_settingsSystem.Active)
                    {
                        _settingsSystem.CloseSettings();
                        return false;
                    }
                }
                HideMenu();
            }
            else
            {
                if (!onlyHide)
                    ShowMenu();
            }
            _stateUI?.ChangeActiveUI(Active);
            return true;
        }
    }
    [System.Serializable]
    public class UIMenu : UIMenuBase
    {
        private bool _isEnd;
        public void SetIsEnd(bool value)
        {
            _isEnd = value;
            if (_isEnd)
                ChangeActiveBase(true);
        }
        [SerializeField] private Button _continue;
        protected override void InitLocal()
        {
            _continue.onClick.AddListener(Continue);
        }
        protected override void DisposeLocal()
        {
            _continue.onClick.RemoveListener(Continue);
        }
        protected override void BindInput()
        {
            _input.EventEscape += ChangeActive;
        }
        protected override void UnbindInput()
        {
            _input.EventEscape -= ChangeActive;
        }
        private void ChangeActive()
        {
            if (_isEnd)
                return;
            ChangeActiveBase();
        }
        private void Continue()
        {
            ChangeActiveBase(true);
        }
    }
}