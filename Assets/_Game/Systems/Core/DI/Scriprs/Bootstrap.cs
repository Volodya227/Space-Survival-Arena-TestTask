using UnityEngine;
namespace Systems.Core.DI
{
    public class Bootstrap : MonoBehaviour
    {
        private LoaderScene _loaderScene;
        private static Bootstrap _instance;
        public static GlobalContainerSystems Get => (_instance == null) ? null : _instance._globalContainerSystems;
        private GlobalContainerSystems _globalContainerSystems;
        private ApplicationSettings _settings;
        private Data.ApplicationData.ApplicationData _applicationData;
        [SerializeField] private Player.Inputs.PlayerInput _playerInputPrefab;
        [SerializeField] private UI.LoaderView.UI_LoaderView _UI_loaderView;
        private Save.SaveSystem _saveSystem;
        private Player.Inputs.PlayerInput _playerInput;
        private BootstrapEvents _bootstrapEvents;
        private void Awake()
        {
            //singleton
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //bootstrap logic when have not public Instance
            _bootstrapEvents = new BootstrapEvents();
            _bootstrapEvents.EventLoadScene += LoadSceneAsync;
            _bootstrapEvents.EventQuitApplication += Exit;
            _loaderScene = new LoaderScene(_UI_loaderView);

            _saveSystem = new Save.SaveSystem(new Save.JsonSaveProvider());
            _saveSystem.GetData();//TODO async
            //Init another systems
            Init();
        }
        private void Init()
        {
            _applicationData = new Data.ApplicationData.ApplicationData(_saveSystem.GetApplicationDataDTO);
            _applicationData.GetGraphicsReadonly.EventOnChanged += _saveSystem.Save;
            _settings = new ApplicationSettings(_applicationData.GetGraphicsReadonly);
            _playerInput = Instantiate(_playerInputPrefab, parent: transform);
            //Register global systems
            _globalContainerSystems = new GlobalContainerSystems(_playerInput, _applicationData, _bootstrapEvents);
            LoadSceneAsync(1);// go to main menu
        }
        private void OnDestroy()
        {
            //when shuting down PC, this logic lose the data without saving
            if (_instance == this)
            {
                _applicationData.GetGraphicsReadonly.EventOnChanged -= _saveSystem.Save;
                _settings.Dispose();
                _bootstrapEvents.EventLoadScene -= LoadSceneAsync;
                _bootstrapEvents.EventQuitApplication -= Exit;
                //_saveSystem.Delete();//using Delete when to change way for saving data
                //TODO _saveSystem.Dispose(); for closing database, or file.
            }
        }
        private void LoadSceneAsync(int sceneID)
        {
            StartCoroutine(_loaderScene.LoadSceneCoroutine(sceneID));
        }
        private void Exit()
        {
            Application.Quit();
        }
    }
    sealed class LoaderScene
    {
        private readonly UI.LoaderView.UI_LoaderView _loaderViewUI;
        public LoaderScene(UI.LoaderView.UI_LoaderView loaderViewUI)
        {
            _loaderViewUI = loaderViewUI;
        }
        public System.Collections.IEnumerator LoadSceneCoroutine(int sceneID)
        {
            _loaderViewUI.ShowLoadingScreen();
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                _loaderViewUI.ChangeProgress(asyncOperation.progress);
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            _loaderViewUI.ChangeProgress(1);
            _loaderViewUI.HideLoadingScreen();
        }
    }
    class ApplicationSettings
    {
        private readonly Resolution[] _resolutions;
        private readonly Data.ApplicationData.IApplicationDataGraphicsReadonly _data;
        public ApplicationSettings(Data.ApplicationData.IApplicationDataGraphicsReadonly data = null)
        {
            _data = data;
            _resolutions = Screen.resolutions;
            if (_data != null)
            {
                _data.EventOnChanged += Update;
                Update();
            }
        }
        public void Dispose()
        {
            if (_data != null)
            {
                _data.EventOnChanged -= Update;
            }
        }
        private void SetFrameRate(int value)
        {
            Application.targetFrameRate = value;
        }
        private void SetQualityLevel(int value)
        {
            QualitySettings.SetQualityLevel(Mathf.Clamp(value, 0, QualitySettings.count - 1));
        }
        private void SetResolution(int index, bool isFullScreen)
        {
            index = Mathf.Clamp(index, 0, _resolutions.Length - 1);
            Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, isFullScreen);
        }
        private void Update()
        {
            SetFrameRate(_data.FrameRate);
            SetQualityLevel(_data.QualityIndex);
            SetResolution(_data.ResolutionIndex, _data.IsFullScreen);
        }
    }
}