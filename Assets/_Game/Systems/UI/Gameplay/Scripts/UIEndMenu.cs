using UnityEngine;
using UnityEngine.UI;
namespace Systems.UI.Gameplay
{
    [System.Serializable]
    public class UIEndMenu : UIMenuBase
    {
        public event System.Action EventExitPanel;
        [SerializeField] private Text _textScore;
        [SerializeField] private Button _restart;
        protected override void InitLocal()
        {
            _restart.onClick.AddListener(Restart);
        }
        protected override void DisposeLocal()
        {
            _restart.onClick.RemoveListener(Restart);
        }
        public void ShowMenu(float score = 0)
        {
            _textScore.text = score.ToString();
            ChangeActiveBase();
        }
        private void Restart()
        {
            if(ChangeActiveBase(true))
                EventExitPanel.Invoke();
        }
    }
}