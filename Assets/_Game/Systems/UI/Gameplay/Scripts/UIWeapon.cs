using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay.WeaponViewData
{
    [System.Serializable]
    public class UIWeapon
    {
        [SerializeField] private Transform _panel;
        private Weapon.ContainerData.IWeaponContainerData _state;
        [SerializeField] private Text _reloadingTime;
        [SerializeField] private Text _ProjectileCountValue;
        [SerializeField] private Text _ProjectileCountMaxValue;
        public void SetState(Weapon.ContainerData.IWeaponContainerData state)
        {
            if (_state != null)
            {
                _state.EventChangeProjectileCount -= ChangeProjectileCount;
                _state.EventChangeProjectileCountMax -= ChangeProjectileCountMax;
            }
            _state = state;
            if (_state != null)
            {
                _state.EventChangeProjectileCount += ChangeProjectileCount;
                _state.EventChangeProjectileCountMax += ChangeProjectileCountMax;
                UpdateData();
            }
            _panel.gameObject.SetActive(_state != null);
        }
        private void UpdateData()
        {
            ChangeReloadingTime();
            ChangeProjectileCount();
            ChangeProjectileCountMax();
        }
        public void Update() {
            if (_state != null) {
                ChangeReloadingTime();
            }
        }
        private void ChangeReloadingTime()
        {
            _reloadingTime.text = _state.ReloadingTime.ToString();
        }
        private void ChangeProjectileCount()
        {
            _ProjectileCountValue.text = _state.ProjectileCount.ToString();
        }
        private void ChangeProjectileCountMax()
        {
            _ProjectileCountMaxValue.text = _state.ProjectileCountMax.ToString();
        }
        public void Dispose()
        {
            SetState(null);
        }
    }
}