using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Settings
{
    //SRP proplem use UI, set setting without another layer
    //REDO Settings move to another layer and save data as signal between view and logic
    //TODO local state
    //when apply, callBack event on change settings
    [System.Serializable]
    public class UISettingsGraphicsModule
    {
        [SerializeField] private Dropdown _resolutionDropdown;
        [SerializeField] private Dropdown _frameRateDropdown;
        [SerializeField] private Dropdown _qualityDropdown;
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Button _saveButton;
        private Resolution[] _resolutions;
        private readonly int[] _frameRates = { -1, 20, 30, 40, 50, 60, 120 };
        [SerializeField] private Transform _panel;
        private Data.ApplicationData.IApplicationDataGraphicsSetter _data;
        private bool _isFullScreen;
        private int _resolutionIndex;
        private int _frameRate;
        private int _qualityIndex;
        public void SetActive(bool value)
        {
            _panel.gameObject.SetActive(value);
        }
        public void Init(Data.ApplicationData.IApplicationDataGraphicsSetter data = null)
        {
            _data = data;
            List<string> options = new();
            _resolutions = Screen.resolutions;
            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio + "Hz";
                options.Add(option);
                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            _resolutionDropdown.ClearOptions();
            _frameRateDropdown.ClearOptions();
            _qualityDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(options);

            _qualityDropdown.AddOptions(QualitySettings.names.ToList());
            List<string> frameRatesName = new();
            foreach(int i in _frameRates) { frameRatesName.Add(i.ToString()); }
            _frameRateDropdown.AddOptions(frameRatesName);
            LoadSettings(currentResolutionIndex);
            _resolutionDropdown.value = _resolutionIndex;
            _qualityDropdown.value = _qualityIndex;
            for (int i = 0; i < _frameRates.Length; i++) {
                if (_frameRates[i] == _frameRate)
                {
                    _frameRateDropdown.value = i;
                    break;
                }
            }
            _resolutionDropdown.RefreshShownValue();
            _qualityDropdown.RefreshShownValue();
            _frameRateDropdown.RefreshShownValue();
            _fullScreenToggle.SetIsOnWithoutNotify(_isFullScreen);

            _resolutionDropdown.onValueChanged.AddListener(SetResolution);
            _qualityDropdown.onValueChanged.AddListener(SetQuality);
            _saveButton.onClick.AddListener(SaveSettings);
            _frameRateDropdown.onValueChanged.AddListener(SetFrameRate);
            _fullScreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
        public void Dispose()
        {
            _resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
            _qualityDropdown.onValueChanged.RemoveListener(SetQuality);
            _saveButton.onClick.RemoveListener(SaveSettings);
            _frameRateDropdown.onValueChanged.RemoveListener(SetFrameRate);
            _fullScreenToggle.onValueChanged.RemoveListener(SetFullscreen);
        }
        private void SetFullscreen(bool isFullscreen)
        {
            _isFullScreen = isFullscreen;
        }
        private void SetFrameRate(int frameRateIndex) {
            _frameRate = _frameRates[frameRateIndex];
        }
        private void SetResolution(int resolutionIndex)
        {
            _resolutionIndex = resolutionIndex;
        }
        private void SetQuality(int qualityIndex)
        {
            _qualityIndex = qualityIndex;
        }
        private void SaveSettings()
        {
            if (_data == null) return;
            _data.Update(_isFullScreen, _resolutionIndex, _qualityIndex, _frameRate);
        }
        private void LoadSettings(int currentResolutionIndex)
        {
            if (_data != null)
            {
                _qualityIndex = _data.QualityIndex;
                _resolutionIndex = _data.ResolutionIndex;
                _isFullScreen = _data.IsFullScreen;
                _frameRate = _data.FrameRate;
            }
            else
            {
                _qualityIndex = 3;
                _resolutionIndex = currentResolutionIndex;
                _isFullScreen = true;
                _frameRate = -1;
            }
        }
    }
}