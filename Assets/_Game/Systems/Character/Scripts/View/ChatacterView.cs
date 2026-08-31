using UnityEngine;
namespace Systems.Character
{
    [System.Serializable]
    public class ChatacterView
    {
        [SerializeField] private CharacterAnimator _animator;
        private readonly ContainerData.CharacterContainerData _state;
        public ChatacterView(ContainerData.CharacterContainerData state, RuntimeAnimatorController contoller, Animator animator) {
            _state = state;
            _animator = new CharacterAnimator(_state.MovementState, contoller, animator);
        }
        public void Dispose()
        {
            _animator.Dispose();
        }
    }
}