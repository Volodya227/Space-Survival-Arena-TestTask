namespace Systems.UI.Gameplay.Inputs
{
    public interface IUIInputs
    {
        public event System.Action EventOpenMenu;
        public event System.Action EventAttackPressed;
        public event System.Action EventAttackReleased;
        public event System.Action EventReloading;
        public float MoveX { get;}
        public float MoveZ { get; }
        public void Reset();
        public void SetGameplayMode(bool active);
    }
    [System.Serializable]
    public sealed class UIInputs : IUIInputs
    {
        public event System.Action EventOpenMenu;
        public event System.Action EventChangeActive;
        public event System.Action EventAttackPressed;
        public event System.Action EventAttackReleased;
        public event System.Action EventReloading;
        public bool Active { get; private set; }
        public float MoveX { get; private set; }
        public float MoveZ { get; private set; }
        public void SignalMove(float x, float z)
        {
            MoveX = x;
            MoveZ = z;
        }
        public void SignalMoveRelease()
        {
            Reset();
        }
        public void SignalAttackPressed()
        {
            EventAttackPressed?.Invoke();
        }
        public void SignalAttackReleased()
        {
            EventAttackReleased?.Invoke();
        }
        public void SignalReloading()
        {
            EventReloading?.Invoke();
        }
        public void Reset()
        {
            MoveX = 0;
            MoveZ = 0;
        }
        public void SetGameplayMode(bool active)
        {
            Active = active;
            EventChangeActive?.Invoke();
        }
        public void OpenMenu()
        {
            EventOpenMenu.Invoke();
        }
    }
    public class UIInputsNullReference : IUIInputs
    {
        public event System.Action EventOpenMenu
        {
            add { }
            remove { }
        }
        public event System.Action EventAttackPressed
        {
            add { }
            remove { }
        }
        public event System.Action EventAttackReleased
        {
            add { }
            remove { }
        }
        public event System.Action EventReloading
        {
            add { }
            remove { }
        }
        public float MoveX => 0;
        public float MoveZ => 0;
        public void Reset() { }
        public void SetGameplayMode(bool active) { }
    }
}