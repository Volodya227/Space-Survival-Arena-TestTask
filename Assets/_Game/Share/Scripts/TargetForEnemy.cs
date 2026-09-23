using UnityEngine;
namespace Share
{
    public class TargetForEnemy
    {
        public IHealthState HealthState {  get; private set; }
        private readonly Transform _transform;
        public Vector3 GetPosition => _transform.position;
        public TargetForEnemy(IHealthState healthState, Transform transform)
        {
            HealthState = healthState;
            _transform = transform;
        }
    }
}