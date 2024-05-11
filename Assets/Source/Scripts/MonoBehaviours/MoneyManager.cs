using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class MoneyManager
    {
        private const string CoinsKey = "PlayerCoins";
        private const string CrystalsKey = "PlayerCrystals";

        public static void SaveCoins(int coins)
        {
            int currentCoins = LoadCoins();
            PlayerPrefs.SetInt(CoinsKey, currentCoins + coins);
            PlayerPrefs.Save();
        }

        public static void SaveCrystals(int crystals)
        {
            int currentCrystals = LoadCrystals();
            PlayerPrefs.SetInt(CrystalsKey, currentCrystals + crystals);
            PlayerPrefs.Save();
        }

        public static int LoadCoins()
        {
            return PlayerPrefs.GetInt(CoinsKey, 0);
        }
        public static int LoadCrystals()
        {
            return PlayerPrefs.GetInt(CrystalsKey, 0);
        }
    }
}