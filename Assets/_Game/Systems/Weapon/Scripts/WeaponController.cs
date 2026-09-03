using UnityEngine;
using System.Collections;
namespace Systems.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private Coroutine _flashRoutine;
        private enum WeaponState
        {
            Cooldown,
            Reloading
        }
        private readonly ContainerData.WeaponContainerData _weaponContainerData = new();
        public ContainerData.IWeaponContainerData GetWeaponContainerData => _weaponContainerData;
        private bool _isFiring;
        private bool _isAutomated;
        private int _damage;
        private Inputs.WeaponInput _weaponInput;
        private WeaponState _state;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private DecalLifetime _decalLifeTimePrefab;
        [SerializeField] private GameObject _muzzleFlash;
        private float _reloadTime;
        private float _reloadingTime;
        private float _cooldownTime;
        private int _shootDistance;
        //Ammo
        private int _projectileCount;
        private int _projectileMaxCount;
        private void Awake()
        {
            _muzzleFlash.SetActive(false);
            _reloadTime = 6;
            _cooldownTime = .4f;
            _projectileMaxCount = 30;
            _damage = 40;
            _isAutomated = true;
            _shootDistance = 700;
            UpdateState();
            StartReload();
        }
        private void UpdateState()
        {
            _weaponContainerData.SetprojectileCount(_projectileCount);
            _weaponContainerData.SetprojectileCountMax(_projectileMaxCount);
        }
        private void OnDestroy()
        {
            SetInput(null);
        }
        private void Reload()
        {
            _projectileCount = _projectileMaxCount;
            UpdateState();
        }
        private void Cooldown()
        {
        }
        private void Update()
        {
            Timer();
            if (_isFiring)
            {
                TryShoot();
            }
        }
        private void Timer()
        {
            if (_reloadingTime == 0) return;
            _reloadingTime -= Time.deltaTime;
            if (_reloadingTime < 0)
            {
                _reloadingTime = 0;
                if (_state == WeaponState.Reloading) Reload();
                if(_state == WeaponState.Cooldown) Cooldown();
            }
            _weaponContainerData.SetReloadingTime(_reloadingTime);
        }
        private void TryShoot()
        {
            if (_reloadingTime > 0)
                return;
            if (!_isAutomated)
            {
                //if _isAutomated wait Released
                _isFiring = false;
            }

            if (_projectileCount <= 0)
                return;

            Shoot();
        }
        private void Shoot()
        {
            _projectileCount--;
            _weaponContainerData.SetprojectileCount(_projectileCount);
            //Debug.Log("Is shoot");
            ShowMuzzleFlash();
            Ray ray = new(_targetPoint.position, _targetPoint.forward);
            //Debug.DrawRay(_targetPoint.position, _targetPoint.forward * _shootDistance, Color.red, 10);
            if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
            {
                //Debug.Log("Is hit");
                Quaternion rotation = Quaternion.LookRotation(hit.normal);
                Instantiate(_decalLifeTimePrefab, hit.point + hit.normal * 0.01f, rotation);

                Share.ITakeDamageable damageable = hit.collider.GetComponentInParent<Share.ITakeDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_damage);
                }
            }
            if (_projectileCount > 0)
                StartCooldown();
        }
        private void StartReload()
        {
            _state = WeaponState.Reloading;
            _reloadingTime = _reloadTime;
        }
        private void StartCooldown()
        {
            _state = WeaponState.Cooldown;
            _reloadingTime = _cooldownTime;
        }
        public void SetInput(Inputs.WeaponInput weaponInput)
        {
            if(_weaponInput != null)
            {
                _weaponInput.EventAttackPressed -= AttackPressed;
                _weaponInput.EventAttackReleased -= AttackReleased;
                _weaponInput.EventReload -= StartReload;
                _weaponInput.SetActive(false);
            }
            _weaponInput = weaponInput;
            if (_weaponInput != null)
            {
                _weaponInput.EventAttackPressed += AttackPressed;
                _weaponInput.EventAttackReleased += AttackReleased;
                _weaponInput.EventReload += StartReload;
                _weaponInput.SetActive(true);
            }
        }
        private void AttackPressed()
        {
            _isFiring = true;
        }
        private void AttackReleased()
        {
            _isFiring = false;
        }
        private void ShowMuzzleFlash()
        {
            if (_flashRoutine != null)
                StopCoroutine(_flashRoutine);

            _flashRoutine = StartCoroutine(MuzzleFlashRoutine());
        }
        private IEnumerator MuzzleFlashRoutine()
        {
            _muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.03f);
            _muzzleFlash.SetActive(false);
        }
    }
}