using System;
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
            _perkPrice = DataManager.LoadCost();
            perkPriceText.text = _perkPrice.ToString();
            Debug.Log(_coins);
            Debug.Log(_perkPrice);
            if (_coins >= _perkPrice)
            {
                DataManager.SpendCoins(_perkPrice);

                _nextPerkPrice = _perkPrice + 100;
                DataManager.SavePerkCost(_nextPerkPrice);

                perkPriceText.text = DataManager.LoadCost().ToString();
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
                Debug.Log("Успешно");
            }

            signal.RegistryRaise(new OnMoneyChangeSignal());
        }
    }
}