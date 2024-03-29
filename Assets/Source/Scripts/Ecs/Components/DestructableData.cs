using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct DestructableData : IEcsComponent
    {
        public float Maxhealth;
        public float CurrentHealth;
        public GameObject Prefab;
    }
    
}