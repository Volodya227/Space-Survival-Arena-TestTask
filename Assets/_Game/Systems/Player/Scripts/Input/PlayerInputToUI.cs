namespace Systems.Player.Inputs
{
    public class PlayerInputToUI: UI.Gameplay.Inputs.InputToUI
    {
        public void EventEnterActivation()
        {
            EventEnterActivate();
        }
        public void EventEscapeActivation()
        {
            EventEscapeActivate();
        }
        public bool EventAskActiveUIActivation()
        {
            return EventAskActiveUIActivate();
        }
    }
}