namespace CameraView.Inputs
{
    public abstract class CameraViewInput
    {
        public event System.Action EventChangeLockMouseMode;
        public event System.Action EventChangeView;
        public event System.Action EventChangeCameraView;
        private bool _active = false;
        private float _cameraRotationX;
        private float _cameraRotationY;
        protected float _mouseXMove;
        protected float _mouseYMove;
        public bool LockMouseMode { get; private set; }
        public float MouseXMove => _mouseXMove;
        public float MouseYMove => _mouseYMove;
        //TODO
        public float CameraRotationX => _cameraRotationX;
        public float CameraRotationY => _cameraRotationY;
        //END TODO
        public bool Active => _active;
        public void SetActive(bool value) { _active = value; }
        public void SetYRotation(float rotation)
        {
            _cameraRotationY = rotation;
        }
        public void SetXRotation(float rotation)
        {
            _cameraRotationX = rotation;
            EventChangeCameraView?.Invoke();
        }
        protected void ChangeView()
        {
            EventChangeView?.Invoke();
        }
        protected void ChangeLockMouseMode(bool value) {
            LockMouseMode = value;
            EventChangeLockMouseMode?.Invoke();
        }
    }
}