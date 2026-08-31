namespace Systems.Enemy.ContainerData
{
    public interface IEnemyContainerData
    {
        public event System.Action EventChangeWeaponSate;
        public Share.IMovementState MovementState { get; }
        public Share.IHealthState HealthState { get; }
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData { get; }
    }
    public class EnemyContainerData
    {
        public readonly Share.MovementState movementState;
        public readonly Share.HealthState healthState;
        public Share.IMovementState MovementState => movementState;
        public Share.IHealthState HealthState => healthState;
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData { get; private set; }
        public EnemyContainerData(Data.IEnemyData data)
        {
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
    }
}