using UnityEngine;
namespace Systems.Player
{
    public class PlayerSystem : MonoBehaviour
    {
        private Inputs.PlayerInput _input;
        private PlayerController _player;
        private Character.CharacterSystem _characterSystem;
        private UI.Gameplay.UISystem _UI;
        private CameraView.CameraView _cameraView;
        public void Init(Inputs.PlayerInput input, Character.CharacterSystem characterManager, UI.Gameplay.UISystem UI, CameraView.CameraView cameraView)
        {
            _input = input;
            _cameraView = cameraView;
            _characterSystem = characterManager;
            _UI = UI;
        }
        private void Start()
        {
            _player = new PlayerController(_input, _cameraView, _UI);
            _input.SetUI(_UI.GetInput);
            _input.SetActive(true);
            SetCharacter();
            _input.EventChanageCharacter += SetCharacter;
        }
        private void OnDestroy()
        {
            _player.Dispose();
            _input.SetUI(null);
            _input.SetActive(false);
            _input.EventChanageCharacter -= SetCharacter;
            _input.Dispose();
        }
        private void SetCharacter()
        {
            _characterSystem.ChangeCharacter();
            _player.SetCharacter(_characterSystem.GetCharacter);
        }
    }
}