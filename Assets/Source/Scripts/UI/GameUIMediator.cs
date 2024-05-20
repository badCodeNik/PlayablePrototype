using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class GameUIMediator : MonoSignalListener<OnHeroKilledSignal, OnLevelCompletedSignal>
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
            autoTransition.RestartScene();
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