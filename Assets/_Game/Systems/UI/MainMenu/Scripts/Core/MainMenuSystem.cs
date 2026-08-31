using UnityEngine;
namespace Systems.UI.MainMenu.Core
{
    public class MainMenuSystem : MonoBehaviour
    {
        public IMainMenuActions GetEvents => _mainMenu;
        [SerializeField] private MainMenu _mainMenu = new();
        private void Awake()
        {
            _mainMenu.Init();
            _mainMenu.EventOnClickSettings += CloseMainMenu;
        }
        private void OnDestroy()
        {
            _mainMenu.EventOnClickSettings -= CloseMainMenu;
            _mainMenu.Dispose();
        }
        private void CloseMainMenu()
        {
            _mainMenu.SetActive(false);
        }
        public void OpenMainMenu()
        {
            _mainMenu.SetActive(true);
        }
    }
}