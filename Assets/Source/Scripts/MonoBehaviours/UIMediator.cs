using System;
using Source.Scripts.UI;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class UIMediator : MonoSignalListener<OnHeroKilledSignal>, IDisposable
    {
        [SerializeField] private GameInitializer gameInitializer;
        [SerializeField] private GameOverPanel restartPanel;
        private float _timer;

        private void Awake()
        {
            restartPanel.Restart.onClick.AddListener(RestartGame);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void RestartGame()
        {
            gameInitializer.Restart();
            restartPanel.Hide();
            _timer = 0;
        }

        protected override void OnSignal(OnHeroKilledSignal data)
        {
           restartPanel.Show(_timer);
        }

        public void Dispose()
        {
            restartPanel.Restart.onClick.RemoveListener(RestartGame);
        }
    }
}