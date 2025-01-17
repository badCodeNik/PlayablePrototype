using Source.EasyECS.Interfaces;
using Source.Scripts.Ecs.ECSeventListeners;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct AttackingData : IEcsComponent
    {
        public float Damage;
        public float AttackDistance;
        public float AttackSpeed;
        public GameObject ProjectilePrefab;
        public float ProjectileSpeed;

        public void InitializeValues(float damage, float attackDistance, float attackSpeed, GameObject projectilePrefab,
            float projectileSpeed)
        {
            Damage = damage;
            AttackDistance = attackDistance;
            AttackSpeed = attackSpeed;
            ProjectilePrefab = projectilePrefab;
            ProjectileSpeed = projectileSpeed;
        }

        
        public void InitializeValues(BonusDamage value)
        {
            Damage += value.Value;
        }

        public void InitializeValues(float damage)
        {
            Damage += damage;
        }
    }
}