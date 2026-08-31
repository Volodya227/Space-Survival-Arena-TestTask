using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Settings
{
    public class UISettingsSystem : MonoBehaviour
    {
        public event System.Action EventDisable;
        private Data.ApplicationData.IApplicationData _dataApplication;
        [SerializeField] private Transform _panel;
        [SerializeField] private UISettingsInputModule _input = new();
        [SerializeField] private UISettingsGraphicsModule _graphics = new();
        [SerializeField] private Button _buttonGraphics;
        [SerializeField] private Button _buttonInput;
        [SerializeField] private Button _buttonExit;
        public bool Active { get; private set; }
        private void SetActive(bool value)
        {
            Active = value;
            _panel.gameObject.SetActive(value);
        }
        private void Awake()
        {
            _buttonGraphics.onClick.AddListener(OpenGraphics);
            _buttonInput.onClick.AddListener(OpenInput);
            _buttonExit.onClick.AddListener(CloseSettings);
        }
        public void SetData(Data.ApplicationData.IApplicationData dataApplication) {
            _dataApplication = dataApplication;
        }
        public void Init()
        {
            if (_dataApplication != null)
                _graphics.Init(_dataApplication.GetGraphicsSetter);
            else
                _graphics.Init();
            Open();
        }
        private void OnDestroy()
        {
            _buttonGraphics.onClick.RemoveListener(OpenGraphics);
            _buttonInput.onClick.RemoveListener(OpenInput);
            _buttonExit.onClick.RemoveListener(CloseSettings);
            _graphics.Dispose();
        }
        public void Open()
        {
            SetActive(true);
            OpenGraphics();//default
        }
        private void Close()
        {
            _input.SetActive(false);
            _graphics.SetActive(false);
        }
        public void CloseSettings()
        {
            Close();
            SetActive(false);
            EventDisable?.Invoke();
        }
        private void OpenGraphics()
        {
            Close();
            _graphics.SetActive(true);
        }
        private void OpenInput()
        {
            Close();
            _input.SetActive(true);
        }
    }
}