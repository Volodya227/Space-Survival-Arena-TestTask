namespace Share
{
    public interface ITakeDamageable
    {
        public void TakeDamage(float damage);
        public void Kill();
    }
    public interface IMovementState
    {
        public event System.Action EventChangeDirectionMoving;
        public float MoveX { get; }
        public float MoveZ { get; }
    }
    public class MovementState : IMovementState
    {
        public event System.Action EventChangeDirectionMoving;
        public float MoveX { get; private set; }
        public float MoveZ { get; private set; }
        public void SetDirectionMoving(float x, float z)
        {
            if (MoveX == x && MoveZ == z) return;
            MoveX = x;
            MoveZ = z;
            EventChangeDirectionMoving?.Invoke();
        }
    }
    public interface IHealthState
    {
        public event System.Action EventChangeHealth;
        public float Health { get; }
        public float MAXHealth { get; }
    }
    public class HealthState : IHealthState
    {
        public event System.Action EventChangeHealth;
        public float Health { get; private set; }
        public float MAXHealth { get; private set; }
        public HealthState(int maxHealth = 100)
        {
            MAXHealth = maxHealth;
            SetHealth(MAXHealth);
        }
        public void SetFullHealth()
        {
            SetHealth(MAXHealth);
        }
        public void SetHealth(float health)
        {
            if (Health == health) return;
            if (health < 0) health = 0;
            if (health > MAXHealth) health = MAXHealth;
            Health = health;
            EventChangeHealth?.Invoke();
        }
        public void TakeDamage(float damage)
        {
            if (Health == 0) return;
            Health -= damage;
            if (Health < 0) Health = 0;
            EventChangeHealth?.Invoke();
        }
    }
}