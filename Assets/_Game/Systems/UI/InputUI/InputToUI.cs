namespace Systems.UI.Gameplay.Inputs
{
    public class InputToUI
    {
        public event System.Action EventEnter;
        public event System.Action EventEscape;
        public event System.Action EventAskActiveUI;
        public bool Active { get; private set; }
        public void SetActive(bool value)
        {
            Active = value;
        }
        protected void EventEnterActivate()
        {
            EventEnter?.Invoke();
        }
        protected void EventEscapeActivate()
        {
            EventEscape?.Invoke();
        }
        protected void EventAskActiveUIActivate()
        {
            EventAskActiveUI?.Invoke();
        }
    }
}