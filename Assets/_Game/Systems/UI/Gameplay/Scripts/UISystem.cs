using UnityEngine;
namespace Systems.UI.Gameplay
{
    public interface IMenuEvents
    {
        public event System.Action EventExitScene;
    }
    public sealed class UISystem : MonoBehaviour
    {
        [SerializeField] private UIMenu _menuUI;
        public IMenuEvents GetMenuEvents => _menuUI;
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
        }
        private void Update()
        {
            _weaponUI.Update();
        }
        public void Init(Data.ApplicationData.IApplicationData applicationData = null)
        {
            _applicationData = applicationData;
            _menuUI.Init(_applicationData);
        }
        private void OnDestroy()
        {
            _menuUI.Dispose();
            SetInput(null);
            _characterUI.Dispose();
            _weaponUI.Dispose();
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
            if (_input != null)
            {
                _input.SetActive(true);
                _input.EventAskActiveUI += SetActiveUI;
            }
        }
        private void SetActiveUI()
        {
            _input.UIActive = _menuUI.Active;
            //in fuature could be inventory
        }

    }
}