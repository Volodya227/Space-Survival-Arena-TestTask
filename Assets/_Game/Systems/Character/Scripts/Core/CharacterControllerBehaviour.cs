using UnityEngine;
namespace Systems.Character {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterControllerBehaviour : MonoBehaviour, Share.ITakeDamageable
    {
        private Coroutine _tickCoroutine;
        [SerializeField] private CharacterConfig _inputComponents = new();
        [SerializeField] private CharacterController _core;
        public CharacterController Core => _core;
        private bool _wasInit = false;
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            _tickCoroutine = StartCoroutine(TickCoroutine());
        }
        public void Init()
        {
            if (_wasInit)
                return;
            _wasInit = true;
            _core = new(_inputComponents, GetComponent<Rigidbody>());
        }
        private void OnDestroy()
        {
            StopCoroutine(_tickCoroutine);
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
        private System.Collections.IEnumerator TickCoroutine()
        {
            while (true)
            {
                _core.DetectResources();

                yield return new WaitForSeconds(3f);
            }
        }
    }
}