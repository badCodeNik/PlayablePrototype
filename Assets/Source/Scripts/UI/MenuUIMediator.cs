using System;
using System.Collections.Generic;
using Source.Scripts.KeysHolder;
using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MenuUIMediator : MonoBehaviour
    {
        [SerializeField] private Purchaser purchaser;
        [SerializeField] private GachaPanelController gachaPanelController;
        [SerializeField] private Signal signal;

        private void Awake()
        {
            gachaPanelController.PurchaseCat.onClick.AddListener(BuyCat);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                DataManager.AddCrystals(100);
            Debug.Log(DataManager.LoadCrystals());
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                DataManager.AddCoins(100);
                signal.RegistryRaise(new OnMoneyChangeSignal());
                Debug.Log(DataManager.LoadCoins());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
                DataManager.ResetData();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                DataManager.ResetCatsBought();
        }

        private void BuyCat()
        {
            if (gachaPanelController.GetRandomCat() == HeroKeys.Null)
            {
                Debug.Log("Коты кончились");
                return;
            }

            purchaser.PurchaseCat(gachaPanelController.GetRandomCat());
        }

        private void OnDisable()
        {
            gachaPanelController.PurchaseCat.onClick.RemoveListener(BuyCat);
        }
    }
}