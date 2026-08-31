using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay.CharacterViewData
{
    [System.Serializable]
    public class UICharacter
    {
        private Character.ContainerData.ICharacterContainerData _state;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Text HealthValue;
        public void SetState(Character.ContainerData.ICharacterContainerData state) {
            if (_state != null) {
                _state.HealthState.EventChangeHealth -= ChangeHealth;
            }
            _state = state;
            if (_state != null)
            {
                _state.HealthState.EventChangeHealth += ChangeHealth;
                UpdateData();
            }
            _panel.SetActive(_state != null);
        }
        private void UpdateData()
        {
            ChangeHealth();
        }
        private void ChangeHealth()
        {
            if (HealthValue.text == null) return;
            float value = _state.HealthState.Health;
            HealthValue.text = value.ToString();
        }
        public void Dispose()
        {
            SetState(null);
        }
    }
}