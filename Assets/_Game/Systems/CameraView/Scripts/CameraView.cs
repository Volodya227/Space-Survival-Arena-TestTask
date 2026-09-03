using UnityEngine;
namespace CameraView
{
    public class ViewData
    {
        public Transform parent;
        public float maxDist;
        public float minDist;
        public bool localView;
        public bool lockX;
        public ViewData(Transform _parent, float _maxDist, float _minDist, bool _localView, bool _lockX)
        {
            parent = _parent;
            maxDist = _maxDist;
            minDist = _minDist;
            localView = _localView;
            lockX = _lockX;
        }
    }
    public class CameraView : MonoBehaviour
    {
        private Vector3 _previousePosition;
        private Vector3 _positionInput;
        private bool IsTopDownView => _currentView == 2;
        private bool _wasLockedCursor;
        public float x;
        public float y;
        private Inputs.CameraViewInput _input;
        private float _maxDist;
        private float _minDist;
        private float _mouseX = 0;
        private float _mouseY = 0;
        public float mouseSensitivity = 2;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _distance;
        private readonly ViewData[] _viewData = {
            new(null, 0, 0, true, true),
            new(null, 0, 0, false, false),
            new(null, -30, -40, false, false)
        };//first, third;
        private int _currentView = -1;
        public ViewData GetFirstViewData => _viewData[0];
        public ViewData GetThirdViewData => _viewData[1];
        public ViewData GetTopDownViewData => _viewData[2];
        private void Start()
        {
            SetLockCursor(true);
        }
        public void SetInput(Inputs.CameraViewInput input)
        {
            InputOff();
            _input = input;
            InputOn();
        }
        private void OnDestroy()
        {
            SetInput(null);
            PrivateSetLockCursor(false);
        }
        private void InputOn()
        {
            if (_input != null)
            {
                _input.EventChangeView += ChangeCamera;
                _input.EventChangeLockMouseMode += SetMouseLockMode;
                SetMouseLockMode();
                _input.SetActive(true);
            }
        }
        private void InputOff()
        {
            if (_input != null)
            {
                _input.EventChangeView -= ChangeCamera;
                _input.EventChangeLockMouseMode -= SetMouseLockMode;
                _input.SetActive(false);
            }
        }
        public void UpdateView(bool setView)
        {
            if (!setView) return;
            _currentView = -1;
            ChangeCamera();
        }
        public void SetLockCursor(bool value)
        {
            if (IsTopDownView)
                _wasLockedCursor = value;
            else
                PrivateSetLockCursor(value);
        }
        public void PrivateSetLockCursor(bool value)
        {
            if (value)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
        public void ResetView()
        {
            _mouseX = 0;
            _mouseY = 0;
            SetInput(0, 0, 0);
        }
        public void ChangeCamera()
        {
            bool wasTopDownView = IsTopDownView;
            _currentView++;
            _currentView %= _viewData.Length;
            _minDist = _viewData[_currentView].minDist;
            _maxDist = _viewData[_currentView].maxDist;
            transform.parent = _viewData[_currentView].parent;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _distance.transform.SetLocalPositionAndRotation(new Vector3(0, 0, _minDist), Quaternion.identity);
            _camera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            if (!wasTopDownView && IsTopDownView)
            {
                _wasLockedCursor = Cursor.lockState == CursorLockMode.Locked;
                PrivateSetLockCursor(false);
                SetInput(50, 45, 0);
            }
            else if (wasTopDownView && !IsTopDownView)
            {
                PrivateSetLockCursor(_wasLockedCursor);
            }
            //ResetView();
        }
        private void SetMouseLockMode()
        {
            SetLockCursor(_input.LockMouseMode);
        }
        public void SetInput(float x = 0, float y = 0, float z = 0)
        {
            if (_viewData[_currentView].localView)
                transform.localRotation = Quaternion.Euler(x, y, 0);
            else
                transform.rotation = Quaternion.Euler(x, y, 0);
            _camera.transform.localRotation = Quaternion.Euler(0, 0, z);
        }
        private void Rotate()
        {
            if (IsTopDownView)
            {
                SetInput(50, 45, 0);
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _positionInput = hit.point;

                    if (_previousePosition != _positionInput)
                    {
                        _previousePosition = _positionInput;
                        Vector3 direction = _positionInput - transform.position;
                        Vector3 xz = new(direction.x, 0f, direction.z);

                        _mouseY = Mathf.Atan2(xz.x, xz.z) * Mathf.Rad2Deg;
                        _mouseX = Mathf.Atan2(direction.y, xz.magnitude) * Mathf.Rad2Deg;
                    }
                }
                return;
            }
            float mouseX = _input.MouseXMove * mouseSensitivity;
            float mouseY = _input.MouseYMove * mouseSensitivity;
            _mouseX -= mouseY;
            _mouseY += mouseX;
            _mouseX = Mathf.Clamp(_mouseX, -90, 90);
            _mouseY %= 360;
            SetInput(_mouseX, _mouseY, 0);
            x = _input.MouseXMove;
            y = _input.MouseYMove;
        }
        private void Update()
        {
            if (_input != null)
            {
                Rotate();
                _input.SetYRotation(_mouseY);
                _input.SetXRotation(_mouseX);
            }
        }
    }
}
