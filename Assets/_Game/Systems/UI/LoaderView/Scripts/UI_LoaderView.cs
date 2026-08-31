using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.LoaderView
{
    public class UI_LoaderView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;
        private void Awake()
        {
            ChangeProgress(0);
            HideLoadingScreen();
        }
        public void ChangeProgress(float progress)
        {
            _image.fillAmount = progress;
            _text.text = (progress * 100).ToString();
        }
        public void ShowLoadingScreen()
        {
            _canvas.gameObject.SetActive(true);
        }
        public void HideLoadingScreen()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}