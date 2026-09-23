using UnityEngine;
namespace Systems.Enemy.Attack
{
    public class EnemyAttackController
    {
        private readonly ContainerData.EnemyContainerData _containerData;
        private readonly float _attackCooldown;
        private readonly float _damage;
        private float _currentCooldown;
        private Inputs.EnemyInput _input;
        private readonly Transform _pointOfAttack;
        private readonly float _distanceDamage;
        private readonly LayerMask _layerMask;
        public EnemyAttackController(ContainerData.EnemyContainerData containerData, _Data.IEnemyData data, Transform pointOfAttack, LayerMask layerMask)
        {
            _containerData = containerData;

            if (data != null)
            {
                _attackCooldown = data.AttackCooldown;
                _damage = data.Damage;
                _distanceDamage = data.DistanceDamage;
            }
            else
            {
                _attackCooldown = 15;
                _damage = 5;
                _distanceDamage = 2;
            }
            _containerData.SetDistanceAttack(_distanceDamage);
            _currentCooldown = 0f;
            _pointOfAttack = pointOfAttack;
            _layerMask = layerMask;

            _containerData.SetCanAttack(true);
        }
        public void SetInput(Inputs.EnemyInput input)
        {
            if (_input != null)
            {
                _input.EventAttack -= Attack;
            }
            _input = input;
            if (_input != null)
            {
                _input.EventAttack += Attack;
            }
        }
        public void Tick()
        {
            if (_containerData.CanAttack)
                return;

            _currentCooldown -= Time.fixedDeltaTime;

            if (_currentCooldown <= 0f)
            {
                _currentCooldown = 0f;
                _containerData.SetCanAttack(true);
            }
        }

        public void Attack()
        {
            if (!_containerData.CanAttack)
                return;

            _containerData.SetCanAttack(false);
            _currentCooldown = _attackCooldown;
            if (Physics.Raycast(_pointOfAttack.position, _pointOfAttack.forward, out RaycastHit hit, 3, _layerMask))
            {
                Share.ITakeDamageable damageable = hit.collider.GetComponentInParent<Share.ITakeDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }
}