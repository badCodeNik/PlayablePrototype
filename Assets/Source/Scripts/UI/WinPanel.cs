using System;
using DG.Tweening;
using Source.Scripts.MonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private TextMeshProUGUI crystals;
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private Image panelImage;
        [SerializeField] private Image backGroundImage;
        private Tween _tween;
        private bool _isLevelFinished;
        private float _startTime;
        private TimeSpan _timeTaken;

        private int _coins;
        
        public Button Restart => restart;
        public Button MainMenu => mainMenuButton;

        public void Show(float timeFromStart )
        {
            gameObject.SetActive(true);
            _tween = panelImage.DOFade(1, 1);
            _tween = backGroundImage.DOFade(1, 2);
            crystals.text = DataManager.LoadCrystals().ToString();
            coins.text = DataManager.LoadCoins().ToString();
            timer.text = timeFromStart.ToString("F2");
        }
        

        public void Hide() => gameObject.SetActive(false);
        
        void OnDestroy() 
        {
            Debug.Log("WinPanel was destroyed");
        }
    }
}