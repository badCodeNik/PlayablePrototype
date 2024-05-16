using Source.Scripts.UI;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class UIMediator : MonoSignalListener<OnHeroKilledSignal, OnLevelCompletedSignal>
    {
        [SerializeField] private GameInitializer gameInitializer;
        [SerializeField] private GameOverPanel restartPanel;
        [SerializeField] private WinPanel winningPanel;
        [SerializeField] private AutoTransition autoTransition;
        private float _timer;

        private void Awake()
        {
            restartPanel.Restart.onClick.AddListener(RestartGame);
            winningPanel.Restart.onClick.AddListener(RestartGame);
            winningPanel.MainMenu.onClick.AddListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            autoTransition.Transit();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void RestartGame()
        {
            gameInitializer.Restart();
            restartPanel.Hide();
            winningPanel.Hide();
            _timer = 0;
        }

        protected override void OnSignal(OnHeroKilledSignal data)
        {
            if (restartPanel != null)
            {
                restartPanel.Show(_timer);
            }
        }

        protected override void OnSignal(OnLevelCompletedSignal data)
        {
            if (winningPanel != null)
            {
                winningPanel.Show(_timer);
            }
        }

        public new void OnDisable()
        {
            restartPanel.Restart.onClick.RemoveListener(RestartGame);
            winningPanel.Restart.onClick.RemoveListener(RestartGame);
            winningPanel.MainMenu.onClick.RemoveListener(GoToMainMenu);
        }
    }
}