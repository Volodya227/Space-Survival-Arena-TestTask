namespace Systems.Weapon.Inputs
{
    public abstract class WeaponInput
    {
        public event System.Action EventAttackPressed;
        public event System.Action EventAttackReleased;
        public event System.Action EventReload;
        public bool Active { get; private set; }
        public void SetActive(bool value) {
            Active = value;
        }
        protected void OnAttackPressed()
        {
            EventAttackPressed?.Invoke();
        }
        protected void OnAttackReleased()
        {
            EventAttackReleased?.Invoke();
        }
        protected void OnReload()
        {
            EventReload?.Invoke();
        }
    }
}