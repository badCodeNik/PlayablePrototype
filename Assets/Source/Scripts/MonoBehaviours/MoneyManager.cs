using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class MoneyManager
    {
        private const string CoinsKey = "PlayerCoins";
        private const string CrystalsKey = "PlayerCrystals";

        public static void SaveCoins(int coins)
        {
            PlayerPrefs.SetInt(CoinsKey, coins);
            PlayerPrefs.Save();
        }

        public static void SaveCrystals(int crystals)
        {
            PlayerPrefs.SetInt(CrystalsKey,crystals);
            PlayerPrefs.Save();
        }

        public static int LoadPoints()
        {
            return PlayerPrefs.GetInt(CoinsKey, 0);
        }
    }
}