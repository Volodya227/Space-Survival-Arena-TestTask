using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Systems.UI.Gameplay.Inputs
{
    public sealed class UIInputAdapter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private bool _active;
        [SerializeField] private GameObject _inputObject;
        [Header("Joystick")]
        [SerializeField] private RectTransform _joystickFrame;
        [SerializeField] private RectTransform _joystickHandle;
        [SerializeField] private float _radius = 60f;

        [Header("Fire Button")]
        [SerializeField] private RectTransform _fireButton;
        [Header("Reload Button")]
        [SerializeField] private RectTransform _reloadButton;
        private ReloadButtonRole _reload;
        [SerializeField] private Canvas _canvas;
        private Camera _uiCamera;

        private UIInputs _inputs;

        private JoystickRole _joystick;
        private FireButtonRole _fire;
        [SerializeField] private Button _buttonOpenMenu;

        public void Init(UIInputs inputs)
        {
            if (_inputs != null) {
                _inputs.EventChangeActive -= SetActivate;
            }
            _inputs = inputs;
            if (_inputs != null)
            {
                _inputs.EventChangeActive += SetActivate;
            }
            SetActivate();
            _joystick = new JoystickRole(_joystickFrame, _joystickHandle, _radius, _inputs, _uiCamera);
            _fire = new FireButtonRole(_fireButton, _inputs, _uiCamera);
            _reload = new ReloadButtonRole(_reloadButton, _inputs, _uiCamera);
        }
        private void Awake()
        {
            _uiCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
            if (_inputs != null)
                _buttonOpenMenu.onClick.AddListener(_inputs.OpenMenu);
        }
        private void OnDestroy()
        {
            if(_inputs == null) return;
            _inputs.EventChangeActive -= SetActivate;
            if (_inputs != null)
                _buttonOpenMenu.onClick.RemoveListener(_inputs.OpenMenu);
        }
        public void OnPointerDown(PointerEventData e)
        {
            if(!_active) return;
            if (_joystick.IsTarget(e))
                _joystick.OnPointerDown(e);
            else if (_fire.IsTarget(e))
                _fire.OnPointerDown(e);
            else if (_reload.IsTarget(e))
                _reload.OnPointerDown(e);
        }
        public void OnDrag(PointerEventData e)
        {
            if (!_active) return;
            _joystick.OnDrag(e);
        }
        public void OnPointerUp(PointerEventData e)
        {
            if (!_active) return;
            _joystick.OnPointerUp(e);
            _fire.OnPointerUp(e);
        }
        private void SetActivate()
        {
            if (_inputs == null) return;
            _active = _inputs.Active;
            _inputObject.SetActive(_active);
        }
    }
}
