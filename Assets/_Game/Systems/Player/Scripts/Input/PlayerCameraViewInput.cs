namespace Systems.Player.Inputs
{
    public class PlayerCameraViewInput : CameraView.Inputs.CameraViewInput
    {
        public void SetXY(float x, float y)
        {
            _mouseXMove = x;
            _mouseYMove = y;
        }
        public void ActivateChangeView()
        {
            ChangeView();
        }
        public void SetMouseLockMode(bool value) {
            ChangeLockMouseMode(value);
        }
    }
}