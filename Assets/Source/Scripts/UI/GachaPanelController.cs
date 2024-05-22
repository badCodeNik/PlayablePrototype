using System.Collections.Generic;
using System.Linq;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.Scripts.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Source.Scripts.UI
{
    public class GachaPanelController : MonoBehaviour
    {
        [SerializeField] private Button purchaseCat;

        public Button PurchaseCat => purchaseCat;


        public HeroKeys GetRandomCat()
        {
            var availableKeysLibrary = new List<HeroKeys>();
            var heroLibrary = Libraries.HeroPrefabLibrary.GetAllPacks();
            foreach (var hero in heroLibrary.Where(hero => !IsCatPurchased(hero.ID)))
            {
                availableKeysLibrary.Add(hero.ID);
            }

            Debug.Log(availableKeysLibrary.Count);
            if (availableKeysLibrary.Count == 0) return HeroKeys.Null;
            var randomIndex = Random.Range(0, availableKeysLibrary.Count);
            return availableKeysLibrary[randomIndex];
        }

        private bool IsCatPurchased(HeroKeys heroKeys)
        {
            var isPurchased = DataManager.LoadCatsBought(heroKeys) == 1;
            return isPurchased;
        }
    }
}