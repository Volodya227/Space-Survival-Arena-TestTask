using UnityEngine;
namespace Systems.Character
{
    public class CharacterRotation
    {
        private readonly Transform _transform;
        private readonly float _maxRotationSpeed;
        private Inputs.CharacterInput _input;
        public CharacterRotation(Transform transform)
        {
            _transform = transform;
            _maxRotationSpeed = 120;
        }
        public void SetInput(Inputs.CharacterInput input)
        {
            _input = input;
        }
        public void Rotate()
        {
            float current = _transform.localEulerAngles.y;
            float target = _input.Yrotation;

            float maxDelta = _maxRotationSpeed * Time.deltaTime;

            float delta = Mathf.DeltaAngle(current, target);

            if (Mathf.Abs(delta) <= maxDelta) current = target;
            else current += Mathf.Sign(delta) * maxDelta;

            _transform.localRotation = Quaternion.Euler(0, current, 0);
        }
    }
}