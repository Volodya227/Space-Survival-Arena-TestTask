using UnityEngine;
namespace Systems.Weapon.Data
{
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private int _shootDistance;
        [SerializeField] private int _projectileMaxCount;
        [SerializeField] private bool _isAutomated;
    }
}