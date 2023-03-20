using UI;
using UnityEngine;

namespace System
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;

        public event Action OnRestartButtonPressedEvent;
        
        public float LiveTimer { get; private set; }

        private void Awake()
        {
            _uiController.SetManager(this);
            StartTimer();
        }

        private void Update()
        {
            if (LiveTimer >= 0)
            {
                LiveTimer += Time.deltaTime;
            }
        }

        private void StartTimer() => LiveTimer = 0;
        private void StopTimer() => LiveTimer = -1;

        public void ShowEndGameScreen()
        {
            _uiController.ShowEndGameScreen();
            StopTimer();
        }

        public void OnRestartButtonPressed()
        {
            OnRestartButtonPressedEvent?.Invoke();
            StartTimer();
        }
    }
}
