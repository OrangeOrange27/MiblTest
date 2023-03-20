using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private const string TIMER_DEFAULT_TEXT = "You survived ";
        
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Button _restartButton;
        
        private UIManager _manager;
        public void SetManager(UIManager manager)
        {
            _manager = manager;
            _restartButton.onClick.AddListener(_manager.OnRestartButtonPressed);
            _restartButton.onClick.AddListener(HideEndGameScreen);
        }

        public void ShowEndGameScreen()
        {
            _mainPanel.SetActive(false);
            
            SetTimerText();
            _endGamePanel.SetActive(true);
        }

        public void HideEndGameScreen()
        {
            _endGamePanel.SetActive(false);
            _timerText.text = "";
            
            _mainPanel.SetActive(true);
        }

        private void SetTimerText()
        {
            var sb = new StringBuilder(TIMER_DEFAULT_TEXT);
            sb.Append(_manager.LiveTimer.ToString());
            sb.Append("s");
            _timerText.text = sb.ToString();
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_manager.OnRestartButtonPressed);
            _restartButton.onClick.RemoveListener(HideEndGameScreen);
        }
    }
}
