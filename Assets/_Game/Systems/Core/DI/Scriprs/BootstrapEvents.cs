namespace Systems.Core.DI
{
    public interface IBootstrapEvents
    {
        public void ActivateEventLoadScene(int value);
        public void ActivateEventQuitApplication();
    }
    public class BootstrapEvents : IBootstrapEvents
    {
        public event System.Action<int> EventLoadScene;
        public event System.Action EventQuitApplication;
        public void ActivateEventLoadScene(int value) => EventLoadScene?.Invoke(value);
        public void ActivateEventQuitApplication() => EventQuitApplication?.Invoke();
    }
}