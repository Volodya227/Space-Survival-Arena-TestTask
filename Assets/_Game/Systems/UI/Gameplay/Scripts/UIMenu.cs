using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay
{
    [System.Serializable]
    public class UIMenu : IMenuEvents
    {
        public event System.Action EventExitScene;
        private bool _active = false;
        public bool Active => _active;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _returnMainMenu;

        [SerializeField] private Systems.UI.Settings.UISettingsSystem _settingsSystemPrefab;
        private Systems.UI.Settings.UISettingsSystem _settingsSystem = null;
        private Data.ApplicationData.IApplicationData _applicationData = null;

        private Inputs.InputToUI _input;

        //TODO activate menu from PlayerInput
        public void Init(Data.ApplicationData.IApplicationData applicationData = null)
        {
            HideMenu();
            _settings.onClick.AddListener(OpenSettings);
            _continue.onClick.AddListener(Continue);
            _returnMainMenu.onClick.AddListener(Exit);
            _applicationData = applicationData;
        }
        public void Dispose()
        {
            _continue.onClick.RemoveListener(Continue);
            _settings.onClick.RemoveListener(OpenSettings);
            _returnMainMenu.onClick.RemoveListener(Exit);
            if (_settingsSystem != null)
            {
                _settingsSystem.EventDisable -= ShowMenu;
            }
            _active = false;
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
            if (_input != null) {
                _input.EventEscape -= ChangeActive;
            }
            _input = input;
            if (_input != null)
            {
                _input.EventEscape += ChangeActive;
            }
        }
        private void ChangeActive()
        {
            if (_active)
            {
                if (_settingsSystem != null) {
                    if (_settingsSystem.Active)
                    {
                        _settingsSystem.CloseSettings();
                        return;
                    }
                }
                HideMenu();
            }
            else
                ShowMenu();
        }
        private void Continue()
        {
            HideMenu();
            _input?.EventSetActiveUIActivate();
        }
    }
}