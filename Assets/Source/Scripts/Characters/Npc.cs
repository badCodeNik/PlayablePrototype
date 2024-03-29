using System;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Enums;
using Source.Scripts.EventSystem;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Characters
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private EnemyChannel enemyChannel;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField, ReadOnly] private int entity;

        public EnemyInfo EnemyInfo => enemyInfo;
        public int Entity => entity;
        
        public void Start()
        {
            EcsInitialize();
            enemyChannel.RaiseEvent(this);
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void EcsInitialize()
        {
            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            componenter.Add<EnemyMark>(entity);
            ref var transformData = ref componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            ref var animatorData = ref componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animator);
            
            // Создаем и прокидываем соотвествующую дату в ECS!
            if (EnemyInfo.Movable.Enabled)
            {
                ref var movableData = ref componenter.Add<MovableData>(entity);
                movableData.InitializeValue( EnemyInfo.Movable.MoveSpeed, EnemyInfo.Movable.RotationSpeed, transform, agent);
                
            }
            
            // И все остальные параметры...
            

            if (EnemyInfo.Destructable.Enabled)
            {
                ref var destructableData = ref componenter.Add<DestructableData>(entity);
                destructableData.CurrentHealth = EnemyInfo.Destructable.Health;
                destructableData.Maxhealth = EnemyInfo.Destructable.MaxHealth;
                destructableData.Prefab = gameObject;
            }

            if (EnemyInfo.Attacking.Enabled)
            {
                ref var attackingData = ref componenter.Add<AttackingData>(entity);
                var projectileInfo = Libraries.ProjectileLibrary.GetByID(EnemyInfo.Attacking.ProjectileID);
                attackingData.InitializeValues(
                    EnemyInfo.Attacking.Damage,
                    EnemyInfo.Attacking.AttackDistance,
                    EnemyInfo.Attacking.AttackSpeed,
                    projectileInfo.Prefab,
                    projectileInfo.Speed);
            }
            
        }
        
        
    }

    
}