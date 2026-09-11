namespace Systems.Player.Inputs
{
    public abstract class PlayerInputCommands
    {
        public abstract bool GetEscape { get; }
        public abstract float GetMoveX { get; }
        public abstract float GetMoveZ { get; }
        public abstract float GetMoveMouseX { get; }
        public abstract float GetMoveMouseY { get; }
        public abstract bool GetMouseUp { get; }
        public abstract bool GetMouseDown { get; }
        public abstract bool GetReloadPressed { get; }
        public abstract bool GetChangeViewKeeping { get; }
        public abstract bool GetChangeActiveUI { get; }
        public abstract bool GetChangeCharacter { get; }
        public virtual void Dispose() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}