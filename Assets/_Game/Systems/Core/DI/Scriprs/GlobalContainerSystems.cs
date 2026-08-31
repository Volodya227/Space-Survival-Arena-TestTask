namespace Systems.Core.DI
{
    public sealed class GlobalContainerSystems
    {
        public Player.Inputs.PlayerInput PlayerInput { get; private set; }
        public Data.ApplicationData.IApplicationData ApplicationData { get; private set; }
        public IBootstrapEvents BootstrapEvents { get; private set; }
        public GlobalContainerSystems(Player.Inputs.PlayerInput playerInput, Data.ApplicationData.IApplicationData applicationData, IBootstrapEvents bootstrapEvents)
        {
            ApplicationData = applicationData;
            PlayerInput = playerInput;
            BootstrapEvents = bootstrapEvents;
        }
    }
}