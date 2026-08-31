using UnityEngine;
namespace Systems.Character
{
    [System.Serializable]
    public class CharacterMovement
    {
        private readonly Rigidbody _body;
        private readonly float _speed;
        private readonly Share.MovementState _state;
        private Inputs.CharacterInput _input;
        private float _x;
        private float _z;
        public CharacterMovement(Share.MovementState state, Rigidbody body)
        {
            _speed = 6;//TODO read from data;
            _state = state;
            _body = body;
            _x = 0;
            _z = 0;
        }
        public void SetInput(Inputs.CharacterInput input)
        {
            _input = input;
            if(_input == null)
            {
                _x = 0;
                _z = 0;
            }
        }
        public void Moving() {
            if (_input != null) {
                _x = _input.MoveX;
                _z = _input.MoveZ;
            }
            //TODO use bool for local moving
            Vector3 direction = _speed * Time.fixedDeltaTime * (_body.transform.right * _x + _body.transform.forward * _z).normalized;
            _body.MovePosition(_body.transform.localPosition + direction);
            _state.SetDirectionMoving(_x, _z);
        }
    }
}