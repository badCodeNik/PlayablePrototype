using Source.EasyECS.Interfaces;
using Source.Scripts.Data;
using Source.Scripts.LibrariesSystem;
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

        public void InitializeValues(float damage, float attackDistance,float attackSpeed, GameObject projectilePrefab, float projectileSpeed)
        {
            Damage = damage;
            AttackDistance = attackDistance;
            AttackSpeed = attackSpeed;
            ProjectilePrefab = projectilePrefab;
            ProjectileSpeed = projectileSpeed;
        }
       
    }
}