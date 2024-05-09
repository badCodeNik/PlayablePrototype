using System;
using Sirenix.OdinInspector;
using Source.Scripts.KeysHolder;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Data
{
    [Serializable]
    [Toggle("enabled")]
    public abstract class Parameter
    {
        [SerializeField] protected bool enabled;

        public bool Enabled => enabled;
    }

    [Serializable]
    public class Movable : Parameter
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] private NavMeshAgent navMeshAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
    }


    [Serializable]
    public class Destructable : Parameter
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;
        public float MaxHealth => maxHealth;

        public float Health => health;
    }

    [Serializable]
    public class Attacking : Parameter
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackSpeed;
        [SerializeField] private ProjectileKeys projectileID;


        public float AttackSpeed => attackSpeed;

        public ProjectileKeys ProjectileID => projectileID;

        public float Damage => damage;

        public float AttackDistance => attackDistance;
    }

    [Serializable]
    public class Untouchable : Parameter
    {
        [SerializeField] private float wanderRadius;
        [SerializeField] private float wanderDelay;

        public float WanderRadius => wanderRadius;

        public float WanderDelay => wanderDelay;
    }
}