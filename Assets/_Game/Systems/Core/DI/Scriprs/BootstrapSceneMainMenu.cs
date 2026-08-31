using UnityEngine;
namespace Systems.Core.DI
{
    public class BootstrapSceneMainMenu : MonoBehaviour
    {
        [SerializeField] private UI.MainMenu.Core.MainMenuSystem _mainMenuSystemPrefab;
        private UI.MainMenu.Core.MainMenuSystem _mainMenuSystem;
        [SerializeField] private UI.Settings.UISettingsSystem _settingsSystemPrefab;
        private UI.Settings.UISettingsSystem _settingsSystem = null;
        private UI.MainMenu.IMainMenuActions _menuActions;
        private IBootstrapEvents _bootstrapEvents;
        private Data.ApplicationData.IApplicationData _applicationData;
        private void Awake()
        {
            if(Bootstrap.Get != null)
            {
                _bootstrapEvents = Bootstrap.Get.BootstrapEvents;
                _applicationData = Bootstrap.Get.ApplicationData;
            }
            _mainMenuSystem = Instantiate(_mainMenuSystemPrefab);
            _menuActions = _mainMenuSystem.GetEvents;
            _menuActions.EventOnClickPlay += Play;
            _menuActions.EventOnClickQuit += Quit;
            _menuActions.EventOnClickSettings += OpenSettings;
        }
        private void OnDestroy()
        {
            _menuActions.EventOnClickPlay -= Play;
            _menuActions.EventOnClickQuit -= Quit;
            _menuActions.EventOnClickSettings -= OpenSettings;
            if (_settingsSystem != null)
            {
                _settingsSystem.EventDisable -= OpenMainMenu;
            }
        }
        private void Play()
        {
            if (_bootstrapEvents == null) return;
            _bootstrapEvents.ActivateEventLoadScene(2);
        }
        private void OpenSettings()
        {
            if (_settingsSystem == null)
            {
                _settingsSystem = Instantiate(_settingsSystemPrefab);
                if (_applicationData != null)
                {
                    _settingsSystem.SetData(_applicationData);
                }
                _settingsSystem.Init();
                _settingsSystem.EventDisable += OpenMainMenu;

            }
            _settingsSystem.Open();
        }
        private void OpenMainMenu()
        {
            _mainMenuSystem.OpenMainMenu();
        }
        private void Quit()
        {
            if (_bootstrapEvents == null)
            {
                Application.Quit();
                return;
            }
            _bootstrapEvents.ActivateEventQuitApplication();
        }
    }
}