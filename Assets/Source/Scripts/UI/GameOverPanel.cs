using DG.Tweening;
using Source.Scripts.MonoBehaviours;
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
        [SerializeField] private Image panelImage;
        private Tween _tween;

        private int _coins;


        public Button Restart => restart;


        public void Show(float timeFromStart)
        {
            gameObject.SetActive(true);
            _tween = panelImage.DOFade(1, 1);
            crystals.text = DataManager.LoadCrystals().ToString();
            coins.text = DataManager.LoadCoins().ToString();
            timer.text = timeFromStart.ToString("F2");
        }

        void OnDestroy()
        {
            Debug.Log("GameOverPanel was destroyed");
        }
    }
}