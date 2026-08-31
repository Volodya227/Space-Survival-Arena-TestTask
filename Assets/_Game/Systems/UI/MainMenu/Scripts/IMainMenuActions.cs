namespace Systems.UI.MainMenu
{
    public interface IMainMenuActions
    {
        public event System.Action EventOnClickPlay;
        public event System.Action EventOnClickSettings;
        public event System.Action EventOnClickQuit;
    }
}