using UnityEngine;
namespace Systems.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour, Share.ITakeDamageable
    {
        public event System.Action<EnemyController> EventDisableObject;// for back to pool
        private Rigidbody _body;//could be doing the moving by transform on plane
        //but physics item need more resources

        //TODO activate and Passive FSM for save to pool
        private Inputs.EnemyInput _enemyInput;
        private ContainerData.EnemyContainerData _containerData;// local data about this object
        private float _speed;
        private _Data.IEnemyData _data;
        private View.EnemyView _view;
        public Inputs.EnemyInput GetInput => _enemyInput;
        public ContainerData.IEnemyContainerData GetContainerData => _containerData;
        [Header("Attack")]
        private Attack.EnemyAttackController _attackController;
        [SerializeField] private LayerMask _layerMaskAttack;
        public int IDType { get; private set; }
        private bool _wasInit = false;
        public void SetData(_Data.IEnemyData data, int type = 0)
        {
            _data = data;
            IDType = type;
        }
        private void Start()
        {
            Init();
        }
        public void Init()
        {
            if (_wasInit)
                return;
            _wasInit = true;
            _body = GetComponent<Rigidbody>();
            if (_data != null)
            {
                _speed = _data.Speed;
            }
            else
            {
                _speed = 1;
            }
            _view = Instantiate(original: _data.Prefab);
            _view.transform.parent = _body.transform;
            _view.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _containerData = new ContainerData.EnemyContainerData(_data);
            _attackController = new Attack.EnemyAttackController(_containerData, _data, _view.PointOfAttack, _layerMaskAttack);
            _containerData.healthState.EventChangeHealth += Death;
            SetInput();
        }
        private void OnDestroy()
        {
            _containerData.healthState.EventChangeHealth -= Death;
            SetInput();//unbind saved input
        }
        public void Respawn()
        {
            _containerData.healthState.SetFullHealth();
            gameObject.SetActive(true);
        }
        public void SetInput(Inputs.EnemyInput enemyInput = null)
        {
            if (_enemyInput != null)
            {
                _enemyInput.SetActive(false);
                //unbind input
            }
            _enemyInput = enemyInput;
            _attackController.SetInput(_enemyInput);
            if (_enemyInput != null)
            {
                _enemyInput.SetActive(true);
                //bind input
            }
        }
        private void FixedUpdate()
        {
            if (_enemyInput != null)
            {
                //can add Moveable class for this abstract enemy
                _body.rotation = Quaternion.Euler(0, _enemyInput.RotationY, 0);
                _body.MovePosition(_body.position + (_enemyInput.MoveX * transform.right + _enemyInput.MoveZ * transform.forward).normalized * _speed * Time.fixedDeltaTime);
            }
            _attackController.Tick();
            _containerData.Position = transform.position;
        }
        public void TakeDamage(float damage)
        {
            _containerData.healthState.TakeDamage(damage);
        }
        public void Kill()
        {
            _containerData.healthState.TakeDamage(_containerData.healthState.MAXHealth);
        }
        private void Death()
        {
            if (_containerData.HealthState.Health == 0)
            {
                gameObject.SetActive(false);
                EventDisableObject?.Invoke(this);
            }
        }
    }
}