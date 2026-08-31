namespace Systems.UI.Gameplay.Inputs
{
    public class InputToUI
    {
        public event System.Action EventEnter;
        public event System.Action EventEscape;
        public event System.Action EventAskActiveUI;
        public event System.Action EventSetActiveUI;
        public bool Active { get; private set; }
        public bool UIActive = false;
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
        protected bool EventAskActiveUIActivate()
        {
            EventAskActiveUI?.Invoke();
            return UIActive;
        }
        public void EventSetActiveUIActivate()
        {
            EventAskActiveUI?.Invoke();
            EventSetActiveUI.Invoke();
        }
    }
}