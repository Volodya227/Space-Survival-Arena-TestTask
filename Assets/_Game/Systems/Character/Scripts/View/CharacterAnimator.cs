using UnityEngine;
namespace Systems.Character
{
    [System.Serializable]
    public class CharacterAnimator
    {
        private float _moveX;
        private float _moveZ;
        private readonly RuntimeAnimatorController _contoller;
        private readonly Animator _animator;
        private readonly Share.IMovementState _state;
        public CharacterAnimator(Share.IMovementState state, RuntimeAnimatorController contoller, Animator animator)
        {
            _state = state;
            _contoller = contoller;
            _animator = animator;
            if (_animator != null)
            {
                _animator.runtimeAnimatorController = _contoller;
            }
            UpdateMoving();
            _state.EventChangeDirectionMoving += UpdateMoving;
        }
        public void Dispose()
        {
            _state.EventChangeDirectionMoving -= UpdateMoving;
        }
        private void UpdateMoving()
        {
            _moveX = _state.MoveX;
            _moveZ = _state.MoveZ;
            if (_animator == null) return;
            _animator.SetFloat("MoveX", _moveX);
            _animator.SetFloat("MoveZ", _moveZ);
        }
    }
}