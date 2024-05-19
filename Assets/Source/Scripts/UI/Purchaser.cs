using Source.Scripts.KeysHolder;
using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Purchaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI perkPriceText;
        [SerializeField] private Signal signal;
        private int _perkPrice;
        private int _nextPerkPrice;
        private const int CatPrice = 100;
        private int _coins;
        private int _crystals;

        private void Start()
        {
            _perkPrice = DataManager.LoadCost();
            perkPriceText.text = _perkPrice.ToString();
        }

        public void PurchasePerk()
        {
            _coins = DataManager.LoadCoins();
            if (_coins >= _perkPrice)
            {
                DataManager.SpendCoins(_coins);

                _nextPerkPrice = _perkPrice + 100;
                DataManager.SavePerkCost(_nextPerkPrice);

                perkPriceText.text = _nextPerkPrice.ToString();
            }

            signal.RegistryRaise(new OnMoneyChangeSignal());
        }


        public void PurchaseCat(HeroKeys catId)
        {
            _crystals = DataManager.LoadCrystals();
            if (_crystals >= CatPrice)
            {
                DataManager.SpendCrystals(CatPrice);
                DataManager.SaveCatsBought(catId);

                // Обновите UI, если нужно
                // В инвентаре появляется кот
            }

            signal.RegistryRaise(new OnMoneyChangeSignal());
        }
    }
}