using UnityEngine;
namespace Systems.UI.Gameplay
{
    public interface IMenuEvents
    {
        public event System.Action EventExitScene;
    }
    public sealed class UISystem : MonoBehaviour
    {
        public event System.Action EventRestartGame;
        [SerializeField] private UIMenu _menuUI;
        [SerializeField] private UIEndMenu _endMenuUI = new();
        private readonly UIState _stateUI = new();
        public IUIState GetUIState => _stateUI;
        public IMenuEvents GetMenuEvents => _menuUI;
        public IMenuEvents GetEndMenuEvents => _endMenuUI;
        [SerializeField] private Inputs.UIInputs _inputUI = new();
        [SerializeField] private Inputs.UIInputAdapter _inputAdapterUI;
        [SerializeField] private WeaponViewData.UIWeapon _weaponUI;
        [SerializeField] private CharacterViewData.UICharacter _characterUI;
        private Data.ApplicationData.IApplicationData _applicationData = null;
        private Inputs.InputToUI _input;
        public WeaponViewData.UIWeapon GetWeaponUI => _weaponUI;
        public CharacterViewData.UICharacter GetCharacterUI => _characterUI;
        public Inputs.IUIInputs GetInput => _inputUI;
        private void Awake()
        {
            _inputAdapterUI.Init(_inputUI);
            _weaponUI.SetState(null);
            _characterUI.SetState(null);
            _endMenuUI.EventExitPanel += HideEndMenu;
        }
        private void Update()
        {
            _weaponUI.Update();
        }
        public void Init(Data.ApplicationData.IApplicationData applicationData = null)
        {
            _applicationData = applicationData;
            _menuUI.Init(_applicationData, _stateUI);
            _endMenuUI.Init(_applicationData, _stateUI);
        }
        private void OnDestroy()
        {
            _menuUI.Dispose();
            _endMenuUI.Dispose();
            SetInput(null);
            _characterUI.Dispose();
            _weaponUI.Dispose();
            _endMenuUI.EventExitPanel -= HideEndMenu;
        }
        public void SetInput(Inputs.InputToUI input = null)
        {
            if (_input != null)
            {
                _input.SetActive(false);
                _input.EventAskActiveUI -= SetActiveUI;
            }
            _input = input;
            _menuUI.SetInput(_input);
            _endMenuUI.SetInput(_input);
            if (_input != null)
            {
                _input.SetActive(true);
                _input.EventAskActiveUI += SetActiveUI;
            }
        }
        private void SetActiveUI()
        {
            _stateUI.ChangeActiveUI(_menuUI.Active || _endMenuUI.Active);
            //in fuature could be inventory
        }
        public void SetActiveEndMenu(float score)
        {
            _menuUI.SetIsEnd(true);
            _endMenuUI.ShowMenu(score);
        }
        private void HideEndMenu()
        {
            _menuUI.SetIsEnd(_endMenuUI.Active);
            if (!_endMenuUI.Active)
                EventRestartGame?.Invoke();
        }
    }
    public interface IUIState
    {
        public event System.Action EventChangeActiveUI;
        public bool ActiveUI { get; }
    }
    public class UIState : IUIState
    {
        public event System.Action EventChangeActiveUI;
        public bool ActiveUI { get; private set; }
        public void ChangeActiveUI(bool value)
        {
            ActiveUI = value;
            EventChangeActiveUI.Invoke();
        }
    }
}