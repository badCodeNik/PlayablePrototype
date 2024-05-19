using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class MenuUIMediator : MonoBehaviour
    {
        [SerializeField] private Purchaser purchaser;
        [SerializeField] private BanishCat banishCat;


        private void Awake()
        {
            banishCat.PurchaseCat.onClick.AddListener(BuyCat);
        }

        private void BuyCat()
        {
            if (banishCat.GetRandomCat() == HeroKeys.Null)
            {
                Debug.Log("Коты кончились");
                return;
            }

            purchaser.PurchaseCat(banishCat.GetRandomCat());
        }

        private void OnDisable()
        {
            banishCat.PurchaseCat.onClick.RemoveListener(BuyCat);
        }
    }
}