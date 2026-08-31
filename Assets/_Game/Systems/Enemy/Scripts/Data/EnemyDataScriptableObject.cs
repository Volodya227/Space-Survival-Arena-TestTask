using UnityEngine;
namespace Systems.Enemy.Data
{
    [CreateAssetMenu(fileName = "data Enemy", menuName = "Enemy/Data")]
    public class EnemyDataScriptableObject : ScriptableObject, IEnemyData
    {
        [SerializeField] private View.EnemyView _gameObject;
        [SerializeField] private float _speed;
        [SerializeField] private int _maxHealth = 250;
        [SerializeField] private float _distanceDamage;
        [SerializeField] private float _damage;
        public float Speed => _speed;
        public int MaxHealth { get; }
        public View.EnemyView Prefab => _gameObject;
        public float DistanceDamage => _distanceDamage;
        public float Damage => _damage;
    }
}