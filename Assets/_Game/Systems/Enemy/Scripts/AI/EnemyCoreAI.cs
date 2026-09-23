using UnityEngine;
namespace Systems.Enemy.AI
{
    [System.Serializable]
    public class EnemyCoreAI
    {
        private Share.TargetForEnemy _target;
        [SerializeField] private EnemyLocalAI[] _enemyInputs;
        public EnemyCoreAI(int count = 5)
        {
            _enemyInputs = new EnemyLocalAI[count];
            for (int i = 0; i < _enemyInputs.Length; i++)
            {
                _enemyInputs[i] = new EnemyLocalAI();
            }
        }
        public void SetTarget(Share.TargetForEnemy target)
        {
            _target = target;
            for (int i = 0; i < _enemyInputs.Length; i++)
            {
                _enemyInputs[i].SetTarget(_target);
            }
        }
        public Inputs.EnemyInput Bind(ContainerData.IEnemyContainerData containerData)
        {
            for (int i = 0; i < _enemyInputs.Length; i++)
            {
                if (!_enemyInputs[i].Active)
                {
                    _enemyInputs[i].BindInput(containerData);
                    return _enemyInputs[i].GetInput;
                }
            }
            return null;
        }
        public void Unbind(Inputs.EnemyInput input) {
            if(input == null)
                return;
            for (int i = 0; i < _enemyInputs.Length; i++)
            {
                if (_enemyInputs[i].HasInput(input))
                {
                    _enemyInputs[i].UnbindInput();
                    break;
                }
            }
        }
        public void Tick()
        {
            for (int i = 0; i < _enemyInputs.Length; i++)
            {
                if (_enemyInputs[i].Active)
                {
                    _enemyInputs[i].Tick();
                }
            }
        }
    }
    public class EnemyLocalAI
    {
        private float _distanceAttack;
        private Share.TargetForEnemy _target;
        private readonly EnemyInputAI _input;
        private ContainerData.IEnemyContainerData _containerData;
        public Inputs.EnemyInput GetInput => _input;
        public bool Active => _input.Active;
        public bool HasInput(Inputs.EnemyInput input) => _input == input;
        //TODO get ContainerData from enemy
        public EnemyLocalAI()
        {
            _input = new EnemyInputAI();
        }
        public void SetTarget(Share.TargetForEnemy target)
        {
            _target = target;
        }
        public void BindInput(ContainerData.IEnemyContainerData containerData)
        {
            _containerData = containerData;
            _distanceAttack = 400;// _containerData.DistanceAttack * _containerData.DistanceAttack;
            //_input.SetActive(true);//Move out side
        }
        public void UnbindInput()
        {
            _input.SetActive(false);
            _containerData = null;
        }
        public void Tick()
        {
            if (_target == null || _target.HealthState.Health == 0)
            {
                _input.SetMove(0, 0);
                return;
            }
            Vector3 direction = _target.GetPosition - _containerData.Position;

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            _input.SetRotation(angle);

            if (direction.sqrMagnitude <= _distanceAttack)
            {
                _input.SetMove(0, 0);
                //atack
                if (_containerData.CanAttack)
                {
                    _input.Attack();
                }
            }
            else
            {
                _input.SetMove(0, 1);
            }
        }
    }
}