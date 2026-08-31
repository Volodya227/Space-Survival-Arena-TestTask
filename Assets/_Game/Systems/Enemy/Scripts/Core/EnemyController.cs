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
        private Data.IEnemyData _data;
        private View.EnemyView _view;
        public int IDType { get; private set; }
        public void SetData(Data.IEnemyData data, int type = 0)
        {
            _data = data;
            IDType = type;
        }
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            if (_data != null)
            {
                _speed = _data.Speed;
            }
            _view = Instantiate(original:_data.Prefab);
            _view.transform.parent = _body.transform;
            _view.transform.localPosition = Vector3.zero;
            _view.transform.localRotation = Quaternion.identity;
            _containerData = new ContainerData.EnemyContainerData(_data);
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
                //unbind input
            }
            _enemyInput = enemyInput;
            if (_enemyInput != null)
            {
                //bind input
            }
        }
        private void FixedUpdate()
        {
            if (_enemyInput != null) {
                //can add Moveable class for this abstract enemy
                _body.MovePosition(_body.position + new Vector3(_enemyInput.MoveX, 0, _enemyInput.MoveZ).normalized * _speed);
            }
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