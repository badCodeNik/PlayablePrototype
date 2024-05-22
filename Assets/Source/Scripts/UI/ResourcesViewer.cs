using Source.Scripts.MonoBehaviours;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ResourcesViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI crystals;

        private void Awake()
        {
            coins.text = DataManager.LoadCoins().ToString();
            crystals.text = DataManager.LoadCrystals().ToString();
        }

        private void Update()
        {
            crystals.text = DataManager.LoadCoins().ToString();
            crystals.text = DataManager.LoadCrystals().ToString();
        }


        /*protected override void OnSignal(OnMoneyChangeSignal data)
        {
            UpdateResources();
        }*/
    }
}