using Source.EasyECS;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class EnemyMovementSystem : EasySystem
    {
        private EcsFilter _inputDataFilter;
        protected override void Initialize()
        {
            _inputDataFilter = World.Filter<InputData>().Inc<EnemyMark>().Exc<DestroyingData>().End();
        }

        protected override void Update()
        {
            foreach (var enemyEntity in _inputDataFilter)
            {
                ref var inputData = ref Componenter.Get<InputData>(enemyEntity);
                ref var movableData = ref Componenter.Get<MovableData>(enemyEntity);
                ref var attackingData = ref Componenter.Get<AttackingData>(enemyEntity);
                
                if (Vector2.Distance(movableData.CharacterTransform.position,inputData.Direction) <= attackingData.AttackDistance || 
                   Componenter.Has<DestroyingData>(enemyEntity)) 
                {
                    movableData.NavMeshAgent.isStopped = true;
                    Componenter.Del<InputData>(enemyEntity);
                }
                else
                {
                    movableData.NavMeshAgent.isStopped = false;
                    movableData.NavMeshAgent.SetDestination(inputData.Direction);
                    RegistryEvent(new OnEnemyMoveEvent()
                    {
                        Entity = enemyEntity,
                        Direction = movableData.NavMeshAgent.velocity.normalized
                            
                    });
                }
            }
        }
        
    }
}