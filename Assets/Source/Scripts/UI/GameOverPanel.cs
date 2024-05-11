using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private TextMeshProUGUI crystals;
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private Image panel;
        private Tween _tween;

        private float _startTime;
        private TimeSpan _timeTaken;

        private int _coins;
        

        public Button Restart => restart;

        public void Show(float timeFromStart)
        {
            gameObject.SetActive(true);
            _tween = panel.DOFade(1, 1);
            crystals.text = PlayerPrefs.GetInt("PlayerCrystals", 0).ToString();
            coins.text = PlayerPrefs.GetInt("PlayerCoins", 0).ToString();
            timer.text = timeFromStart.ToString("F2");
        }

        public void Hide() => gameObject.SetActive(false);
    }
}