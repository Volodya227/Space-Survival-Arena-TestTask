using UnityEngine;
namespace Systems.Character {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterControllerBehaviour : MonoBehaviour, Share.ITakeDamageable
    {
        [SerializeField] private CharacterConfig _inputComponents = new();
        [SerializeField] private CharacterController _core;
        public CharacterController Core => _core;
        private void Awake()
        {
            _core = new(_inputComponents, GetComponent<Rigidbody>());
        }
        private void OnDestroy()
        {
            _core.SetInput(null, null);
            _core.Dispose();
        }
        private void FixedUpdate()
        {
            _core.FixedUpdate();
        }
        private void Update()
        {
            _core.Update();
        }
        public void TakeDamage(float damage)
        {
            _core.TakeDamage(damage);
        }
        public void Kill()
        {

        }
    }
}