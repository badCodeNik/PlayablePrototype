using System;
using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Purchaser : MonoBehaviour
    {
        [SerializeField] private int moneyAmount;
        [SerializeField] private int price;

        private void Start()
        {
            moneyAmount = MoneyManager.LoadPoints();
        }

        public void Purchase()
        {
            if (moneyAmount >= price)
            {
                moneyAmount -= price;
                Debug.Log("Капец ты лох");
                MoneyManager.SavePoints(moneyAmount);
            }
        }
    }
}