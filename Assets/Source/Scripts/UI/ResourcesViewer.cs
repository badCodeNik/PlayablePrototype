using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ResourcesViewer : MonoSignalListener<OnMoneyChangeSignal>
    {
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI crystals;

        private void Awake()
        {
            coins.text = DataManager.LoadCoins().ToString();
            crystals.text = DataManager.LoadCrystals().ToString();
        }


        protected override void OnSignal(OnMoneyChangeSignal data)
        {
            crystals.text = DataManager.LoadCoins().ToString();
            crystals.text = DataManager.LoadCrystals().ToString();
        }
    }
}