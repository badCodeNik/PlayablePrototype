using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct DestructableData : IEcsComponent
    {
        public float Maxhealth;
        public float CurrentHealth;
        public GameObject Prefab;
        public int CoinsForKill;
        public int CrystalsForKill;


        public void InitializeValues(float maxHealth, float currentHealth, GameObject prefab, int coinsForKill,
            int crystalsForKill)
        {
            Maxhealth = maxHealth;
            CurrentHealth = currentHealth;
            Prefab = prefab;
            CoinsForKill = coinsForKill;
            CrystalsForKill = crystalsForKill;
        }
    }
    
}