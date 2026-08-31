using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Settings
{
    [System.Serializable]
    public class UISettingsInputModule
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private Button _keyboardButton;
        [SerializeField] private Button _touchButton;
        public void SetActive(bool value)
        {
            _panel.gameObject.SetActive(value);
        }
        //TODO Get KeyCode from InputMap for change
        //Add struct: {name, button1, button2}
        //when click on button, try change KeyCode on activated space"button"
        //UI text decoding keyCode
    }
}