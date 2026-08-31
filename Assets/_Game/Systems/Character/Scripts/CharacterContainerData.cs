namespace Systems.Character.ContainerData
{
    //for UI set interface
    public interface ICharacterContainerData
    {
        public event System.Action EventChangeWeaponSate;
        public Share.IMovementState MovementState { get; }
        public Share.IHealthState HealthState { get; }
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData { get; }
    }
    public class CharacterContainerData : ICharacterContainerData
    {
        public event System.Action EventChangeWeaponSate;
        public readonly Share.MovementState movementState;
        public readonly Share.HealthState healthState;
        public Weapon.ContainerData.IWeaponContainerData weaponContainerData;
        public Share.IMovementState MovementState => movementState;
        public Share.IHealthState HealthState => healthState;
        public Weapon.ContainerData.IWeaponContainerData WeaponContainerData => weaponContainerData;
        public CharacterContainerData()
        {
            movementState = new Share.MovementState();
            healthState = new Share.HealthState();
        }
        public void SetWeaponContainerData(Weapon.ContainerData.IWeaponContainerData weaponContainerData)
        {
            this.weaponContainerData = weaponContainerData;
            EventChangeWeaponSate?.Invoke();
        }
    }
}