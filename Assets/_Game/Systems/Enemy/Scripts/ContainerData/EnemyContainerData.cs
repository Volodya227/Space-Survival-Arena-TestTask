namespace Systems.Enemy.ContainerData
{
    public interface IEnemyContainerData
    {
        //public event System.Action EventChangeWeaponSate;
        public Share.IMovementState MovementState { get; }
        public Share.IHealthState HealthState { get; }
        public UnityEngine.Vector3 Position { get; }
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData { get; }
        public bool CanAttack { get; }
        public float DistanceAttack { get; }
    }
    public class EnemyContainerData : IEnemyContainerData
    {
        public readonly Share.MovementState movementState;
        public readonly Share.HealthState healthState;
        public UnityEngine.Vector3 Position { get; set; }// for API AI
        public Share.IMovementState MovementState => movementState;
        public Share.IHealthState HealthState => healthState;
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData { get; private set; }
        public bool CanAttack { get; private set; }
        public float DistanceAttack { get; private set; }
        public EnemyContainerData(_Data.IEnemyData data)
        {
            CanAttack = false;
            movementState = new Share.MovementState();
            if (data != null)
                healthState = new Share.HealthState(data.MaxHealth);//data?.MaxHealth don't work
            else
                healthState = new Share.HealthState();
        }
        //copied from character!
        public void SetWeaponContainerData(Weapon.ContainerData.IWeaponContainerData weaponContainerData)
        {
            WeaponContainerData = weaponContainerData;
        }
        public void SetCanAttack(bool canAttack) {
            CanAttack = canAttack;
        }
        public void SetDistanceAttack(float distanceAttack)
        {
            DistanceAttack = distanceAttack;
        }
    }
}