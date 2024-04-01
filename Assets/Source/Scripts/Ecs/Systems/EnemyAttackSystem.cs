using Source.EasyECS;
using Source.Scripts.Characters;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class EnemyAttackSystem : EasySystem
    {
        private EcsFilter _playerFilter;
        private EcsFilter _reloadRemainingFilter;
        private EcsFilter _readyAttackFilter;
        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
            _reloadRemainingFilter = World.Filter<AttackReloadData>().End();
            _readyAttackFilter = World.Filter<EnemyMark>().Exc<DestroyingData>().Exc<AttackReloadData>().End();
        }

        protected override void Update()
        {
            TryReload();
            TryAttack();
        }

        private void TryAttack()
        {
            if (_playerFilter.TryGetFirstEntity(out int playerEntity))
            {
                foreach (var enemyEntity in _readyAttackFilter)
                {
                    ref var attackingData = ref Componenter.Get<AttackingData>(enemyEntity);
                    var playerTransform = Componenter.Get<TransformData>(playerEntity).Value;
                    var enemyTransform = Componenter.Get<TransformData>(enemyEntity).Value;
                    var enemyPosition = enemyTransform.position;
                    var playerPosition = playerTransform.position;
                    var canAttack = attackingData.AttackDistance >
                                    Vector2.Distance(playerPosition, enemyPosition);
                    var targetDirection = ((Vector2)playerPosition - (Vector2)enemyPosition).normalized;
                    var targetPoint = (Vector2)playerPosition - targetDirection * attackingData.AttackDistance;
                    if (!canAttack)
                    {
                        Componenter.Add<InputData>(enemyEntity).InitializeValues(targetPoint);
                    }
                    else
                    {
                        Componenter.Add<AttackReloadData>(enemyEntity).InitializeValues(attackingData.AttackSpeed);
                        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;       
                        var newObject = Object.Instantiate(attackingData.ProjectilePrefab,enemyTransform.position,Quaternion.Euler(0f, 0f, angle));
                        var projectile = newObject.GetComponent<Projectile>();
                        var velocity = targetDirection * attackingData.ProjectileSpeed ;
                        projectile.Initialize((targetEntity =>
                        {
                            RegistryEvent(new OnProjectileTouch
                            {
                                CharacterEntity = enemyEntity,
                                TargetEntity = targetEntity
                            });
                        } ),velocity,CharacterFaction.Enemy);
                    }
                }
                    
            }//else if there is no player, what happens?
            
        }

        private void TryReload()
        {
            foreach (var entity in _reloadRemainingFilter)
            {
                var reloadData =  Componenter.Get<AttackReloadData>(entity);
                reloadData.RemainingTime -= Time.fixedDeltaTime;
                if (reloadData.RemainingTime <= 0)
                {
                    Componenter.Del<AttackReloadData>(entity);
                }
                    
            }
        }
    }
}