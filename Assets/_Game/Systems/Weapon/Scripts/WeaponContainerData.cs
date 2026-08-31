namespace Systems.Weapon.ContainerData
{
    public interface IWeaponContainerData
    {
        public event System.Action EventChangeProjectileCount;
        public event System.Action EventChangeProjectileCountMax;
        public int ProjectileCount { get; }
        public int ProjectileCountMax { get; }
        public float ReloadingTime { get; }
    }
    public class WeaponContainerData : IWeaponContainerData
    {
        public event System.Action EventChangeProjectileCount;
        public event System.Action EventChangeProjectileCountMax;
        public int ProjectileCount { get; private set; }
        public int ProjectileCountMax { get; private set; }
        public float ReloadingTime { get; set; }

        public void SetprojectileCount(int value)
        {
            ProjectileCount = value;
            EventChangeProjectileCount?.Invoke();
        }
        public void SetprojectileCountMax(int value)
        {
            ProjectileCountMax = value;
            EventChangeProjectileCountMax?.Invoke();
        }
        public void SetReloadingTime(float value) {
            ReloadingTime = value;
        }
    }
}