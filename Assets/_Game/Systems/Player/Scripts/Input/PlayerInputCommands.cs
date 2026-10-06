namespace Systems.Player.Inputs
{
    public abstract class PlayerInputCommands
    {
        protected UnityEngine.EventSystems.EventSystem _eventSystem;
        protected UnityEngine.EventSystems.PointerEventData _pointerEventData;
        protected readonly System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> _raycastResults = new();
        public void SetEventSystem(UnityEngine.EventSystems.EventSystem eventSystem) {
            _eventSystem = eventSystem;
            _pointerEventData  = new UnityEngine.EventSystems.PointerEventData(_eventSystem);
        }
        public abstract int PointerId { get; }
        public abstract bool PointerIdIsDefault { get; }
        public abstract bool GetEscape { get; }
        public abstract float GetMoveX { get; }
        public abstract float GetMoveZ { get; }
        public abstract float GetMoveMouseX { get; }
        public abstract float GetMoveMouseY { get; }
        public abstract UnityEngine.Vector3 MousePosition { get; }
        public abstract bool GetMouseUp { get; }
        public abstract bool GetMouseDown { get; }
        public abstract bool GetReloadPressed { get; }
        public abstract bool GetChangeViewKeeping { get; }
        public abstract bool GetChangeActiveUI { get; }
        public abstract bool GetChangeCharacter { get; }
        public virtual void Dispose() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void Tick() { }
        protected bool IsOverUI(UnityEngine.Vector2 position)
        {
            _pointerEventData.position = position;
            _raycastResults.Clear();
            _eventSystem.RaycastAll(_pointerEventData, _raycastResults);
            return _raycastResults.Count > 0;
        }
    }
}