using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.MainMenu
{
    [System.Serializable]
    public class MainMenu : IMainMenuActions
    {
        public event System.Action EventOnClickPlay;
        public event System.Action EventOnClickSettings;
        public event System.Action EventOnClickQuit;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Transform _panel;
        public void SetActive(bool value)
        {
            _panel.gameObject.SetActive(value);
        }
        public void Init()
        {
            _buttonPlay.onClick.AddListener(Play);
            _buttonSettings.onClick.AddListener(Settings);
            _buttonExit.onClick.AddListener(Quit);
        }
        public void Dispose()
        {
            _buttonPlay.onClick.RemoveListener(Play);
            _buttonSettings.onClick.RemoveListener(Settings);
            _buttonExit.onClick.RemoveListener(Quit);
        }
        private void Play()
        {
            EventOnClickPlay?.Invoke();
        }
        private void Quit()
        {
            EventOnClickQuit?.Invoke();
        }
        private void Settings()
        {
            EventOnClickSettings?.Invoke();
        }
    }
}