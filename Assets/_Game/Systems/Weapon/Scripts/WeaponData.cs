using UnityEngine;
namespace Systems.Weapon.Data
{
    [CreateAssetMenu(fileName = "data Weapon", menuName = "Weapon/Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private int _shootDistance;
        [SerializeField] private int _projectileMaxCount;
        [SerializeField] private bool _isAutomated;
        public int Damage => _damage;
        public float ReloadTime => _reloadTime;
        public float CooldownTime => _cooldownTime;
        public int ShootDistance => _shootDistance;
        public int ProjectileMaxCount => _projectileMaxCount;
        public bool IsAutomated => _isAutomated;
    }
}