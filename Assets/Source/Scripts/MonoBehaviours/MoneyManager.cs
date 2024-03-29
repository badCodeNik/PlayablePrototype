using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class MoneyManager 
    {
        private const string MoneyKey = "PlayerMoney";
        
        public static void SaveMoney(int moneyAmount)
        {
            PlayerPrefs.SetInt(MoneyKey, moneyAmount);
            PlayerPrefs.Save();
        }
        
        public static int LoadMoney()
        {
            return PlayerPrefs.GetInt(MoneyKey, 0); 
        }
    }
}