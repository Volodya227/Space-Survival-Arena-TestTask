using UnityEngine;
namespace Systems.Character
{
    [System.Serializable]
    public class CharacterConfig
    {
        public Animator Animation;
        public RuntimeAnimatorController Controller;
        public Transform FirstView;
        public Transform ThirdView;
        public Weapon.WeaponController WeaponControllerPrefab;
        public _Data.CharacterData Data;
        public Transform PivotArmForWeapon;
        public LayerMask layerMaskResources;
    }
    [System.Serializable]
    public class CharacterController
    {
        private bool _isAlive;
        private readonly Rigidbody _body;
        private Weapon.WeaponController _weaponController;
        private Inputs.CharacterInput _input;
        private Weapon.Inputs.WeaponInput _weaponInput = null;
        private readonly _Data.CharacterData _data;
        private readonly ContainerData.CharacterContainerData _state;
        public ContainerData.ICharacterContainerData State => _state;
        private readonly CharacterConfig _components;
        [SerializeField] private ChatacterView _characterView;
        private readonly CharacterMovement _movement;
        private readonly CharacterRotation _rotation;
        private readonly Transform _firstView;
        private readonly Transform _thirdView;
        private readonly Transform _pivotArmForWeapon;
        private readonly Resources.Items.TypeItem _typeItem;
        private LayerMask _layerMaskResources;
        private readonly Collider[] _itemsResource = new Collider[32];
        public Share.TargetForEnemy TargetAboutSelf { get; private set; }
        public Transform FirstView => _firstView;
        public Transform ThirdView => _thirdView;
        public CharacterController(CharacterConfig components, Rigidbody body)
        {
            _pivotArmForWeapon = components.PivotArmForWeapon;
            _data = components.Data;
            _body = body;
            _components = components;
            _typeItem = new Resources.Items.TypeItem(id: 0);
            _state = new ContainerData.CharacterContainerData(_data.MaxHealth, _typeItem);
            _characterView = new ChatacterView(_state, _components.Controller, _components.Animation);
            _movement = new CharacterMovement(_state.movementState, _body, _data);
            _rotation = new CharacterRotation(_body.transform);
            _firstView = _components.FirstView;
            _thirdView = _components.ThirdView;
            _layerMaskResources = components.layerMaskResources;
            SetWeapon(Object.Instantiate(_components.WeaponControllerPrefab));//FIX
            Respawn();
            SetInput(null);
            TargetAboutSelf = new Share.TargetForEnemy(State.HealthState, _body.transform);
        }
        public void Dispose()
        {
            _characterView.Dispose();
        }
        public void SetInput(Inputs.CharacterInput input, Weapon.Inputs.WeaponInput weaponInput = null)
        {
            _input?.SetActive(false);
            _weaponInput?.SetActive(false);
            _input = input;
            _movement.SetInput(_input);
            _rotation.SetInput(_input);
            _weaponInput = weaponInput;
            if (_weaponController != null)
                _weaponController.SetInput(_weaponInput);
            UpdateInputState();
            //TODO reset state if input == null
        }
        private void UpdateInputState()
        {
            _input?.SetActive(_isAlive);
            _weaponInput?.SetActive(_isAlive && _weaponController != null);
        }
        public void Update()
        {
            if (_isAlive)
            {
                if (_input != null)
                {
                    _rotation.Rotate();
                }
            }
        }
        public void FixedUpdate()
        {
            _movement.Moving();
        }
        public void SetWeapon(Weapon.WeaponController weaponController)
        {
            if (_weaponController != null)
            {
                _weaponController.SetInput(null);
            }
            _weaponController = weaponController;
            if (_weaponController != null)
            {
                _state.SetWeaponContainerData(_weaponController.GetWeaponContainerData);
                _weaponController.transform.parent = _pivotArmForWeapon;
                _weaponController.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                _weaponController.SetInput(_weaponInput);
                UpdateInputState();
            }
        }
        public void TakeDamage(float damage) {
            _state.healthState.TakeDamage(damage);
            if (_state.healthState.Health == 0)
            {
                Dead();
            }
        }
        private void Dead()
        {
            _isAlive = false;
            UpdateInputState();
        }
        public void Respawn()
        {
            _state.healthState.SetFullHealth();
            _isAlive = true;
            UpdateInputState();
        }
        public void DetectResources()
        {
            int Count = Physics.OverlapSphereNonAlloc(_body.transform.position, 5, results: _itemsResource, _layerMaskResources);
            for (int i = 0; i < Count;i++)
            {
                if (_itemsResource[i] == null)
                {
                    break;
                }
                Resources.Items.RealTimeObjectItem type = _itemsResource[i].GetComponent<Resources.Items.RealTimeObjectItem>();
                if (type != null)
                {
                    //TODO (_typeItem.ID == type.ID), when typy of ID will be more than one!
                    type.Use(out Data.Resources.Items.TypeItemStruct resource);
                    _typeItem.Add(resource);
                }
                _itemsResource[i] = null;
            }
        }
    }
}