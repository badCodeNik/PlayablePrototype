using System;
using System.Linq;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class DataManager
    {
        private const string CoinsKey = "PlayerCoins";
        private const string CrystalsKey = "PlayerCrystals";
        private const string PerkCostKey = "PerkCost";
        private const string HeroKey = "CatBought";
        private const string SelectedHeroKey = "SelectedCat";

        public static void AddCoins(int amountToAdd)
        {
            int currentCoins = LoadCoins();
            PlayerPrefs.SetInt(CoinsKey, currentCoins + amountToAdd);
            PlayerPrefs.Save();
        }

        public static void SpendCoins(int amountToSpend)
        {
            int currentCoins = LoadCoins();
            PlayerPrefs.SetInt(CoinsKey, currentCoins - amountToSpend); // Вычитаем из имеющихся
            PlayerPrefs.Save();
        }

        public static void AddCrystals(int amountToAdd)
        {
            int currentCrystals = LoadCrystals();
            PlayerPrefs.SetInt(CrystalsKey, currentCrystals + amountToAdd); // Добавляем к имеющимся
            PlayerPrefs.Save();
        }

        public static void SpendCrystals(int amountToSpend)
        {
            int currentCrystals = LoadCrystals();
            PlayerPrefs.SetInt(CrystalsKey, currentCrystals - amountToSpend); // Вычитаем из имеющихся
            PlayerPrefs.Save();
        }

        public static void SavePerkCost(int cost)
        {
            int currentCost = LoadCost();
            PlayerPrefs.SetInt(PerkCostKey, currentCost + cost);
            PlayerPrefs.Save();
        }

        public static void SaveCatsBought(HeroKeys heroEnumKey)
        {
            PlayerPrefs.SetInt(HeroKey + heroEnumKey, 1);
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

        public static int LoadCost()
        {
            return PlayerPrefs.GetInt(PerkCostKey, 100);
        }

        public static int LoadCatsBought(HeroKeys heroKeys)
        {
            return PlayerPrefs.GetInt(HeroKey + heroKeys);
        }

        public static void ResetData()
        {
            PlayerPrefs.SetInt(CrystalsKey, 0);
            PlayerPrefs.SetInt(CoinsKey, 0);
            PlayerPrefs.SetInt(PerkCostKey, 0);
        }

        public static void ResetCatsBought()
        {
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Butler, 0);
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Jokeress, 0);
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Joker, 0);
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Wizard, 0);
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Sorcerer, 0);
            PlayerPrefs.SetInt(HeroKey + HeroKeys.Maid, 0);
        }

        public static void SaveSelectedCat(HeroKeys catKey)
        {
            PlayerPrefs.SetString(SelectedHeroKey, catKey.ToString());
            PlayerPrefs.Save();
        }

        public static HeroKeys LoadSelectedCat()
        {
            string selectedCatKey =
                PlayerPrefs.GetString("SelectedCat", "Jokeress"); // Предполагается, что у вас есть дефолтное значение
            HeroKeys selectedCat;

            if (Enum.TryParse(selectedCatKey, out selectedCat))
            {
                return selectedCat;
            }
            else
            {
                return HeroKeys.Jokeress;
            }
        }
    }
}