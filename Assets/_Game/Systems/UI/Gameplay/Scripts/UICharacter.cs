using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay.CharacterViewData
{
    [System.Serializable]
    public class UICharacter
    {
        private Character.ContainerData.ICharacterContainerData _state;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Text _healthValue;
        [SerializeField] private Text _resourceValue;
        public void SetState(Character.ContainerData.ICharacterContainerData state) {
            if (_state != null) {
                _state.HealthState.EventChangeHealth -= ChangeHealth;
                _state.Item.EventChangeCount -= ChangeResource;
            }
            _state = state;
            if (_state != null)
            {
                _state.HealthState.EventChangeHealth += ChangeHealth;
                _state.Item.EventChangeCount += ChangeResource;
                UpdateData();
            }
            _panel.SetActive(_state != null);
        }
        private void UpdateData()
        {
            ChangeHealth();
            ChangeResource();
        }
        private void ChangeHealth()
        {
            if (_healthValue.text == null) return;
            float value = _state.HealthState.Health;
            _healthValue.text = value.ToString();
        }
        private void ChangeResource()
        {
            if (_resourceValue.text == null) return;
            int value = _state.Item.Count;
            _resourceValue.text = value.ToString();
        }
        public void Dispose()
        {
            SetState(null);
        }
    }
}