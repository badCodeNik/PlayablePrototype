using Source.EasyECS;
using Source.Scripts.Characters;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Enums;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class PlayerAttackSystem : EasySystem
    {
        private EcsFilter _playerFilter;
        private EcsFilter _readyAttackFilter;
        private EcsFilter _projectileFilter;
        private EcsFilter _enemyFilter;
        private EcsFilter _reloadRemainingFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().Exc<InputData>().End();
            _readyAttackFilter = World.Filter<PlayerMark>().Exc<InputData>().Exc<AttackReloadData>().End();
            _enemyFilter = World.Filter<EnemyMark>().Exc<DestroyingData>().End();
            _reloadRemainingFilter = World.Filter<AttackReloadData>().End();
        }

        protected override void Update()
        {
            TryReload();

            TryAttack();
        }

        private void TryAttack()
        {
            foreach (var playerEntity in _readyAttackFilter)
            {
                if (TryGetCloseTarget(playerEntity, out int enemyEntity))
                {
                    ref var attackingData = ref Componenter.Get<AttackingData>(playerEntity);
                    var playerTransform = Componenter.Get<TransformData>(playerEntity).Value;
                    var enemyTransform = Componenter.Get<TransformData>(enemyEntity).Value;
                    ref var reloadData = ref Componenter.Add<AttackReloadData>(playerEntity);
                    var targetDirection = ((Vector2)enemyTransform.position -
                                           (Vector2)playerTransform.position).normalized;
                    reloadData.InitializeValues(attackingData.AttackSpeed);
                    var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                    var newObject = Object.Instantiate(attackingData.ProjectilePrefab, playerTransform.position,
                        Quaternion.Euler(0f, 0f, angle));
                    var projectile = newObject.GetComponent<Projectile>();
                    var velocity = targetDirection * attackingData.ProjectileSpeed;
                    var projectileSprite =
                        Libraries.ProjectileLibrary.GetByID(ProjectileKeys.PlayerDefault).PreviewSprite;
                    projectile.Initialize((targetEntity =>
                    {
                        RegistryEvent(new OnProjectileTouch
                        {
                            CharacterEntity = playerEntity,
                            TargetEntity = targetEntity
                        });
                    }), velocity, CharacterFaction.Player, projectileSprite);
                    //why doesn't it set 1 every time when it ends?
                }
            }
        }

        private bool TryGetCloseTarget(int playerEntity, out int targetEntity)
        {
            float closeDistance = 999f;
            bool hasTarget = false;
            int closeTarget = -1;
            var playerTransform = Componenter.Get<TransformData>(playerEntity).Value;
            foreach (var enemyEntity in _enemyFilter)
            {
                var enemyTransform = Componenter.Get<TransformData>(enemyEntity).Value;
                if (!hasTarget)
                {
                    hasTarget = true;
                    closeTarget = enemyEntity;
                    closeDistance = Vector2.Distance(playerTransform.position, enemyTransform.position);
                    continue;
                }

                var distance = Vector2.Distance(playerTransform.position, enemyTransform.position);
                if (distance < closeDistance)
                {
                    closeDistance = distance;
                    closeTarget = enemyEntity;
                }
            }

            targetEntity = hasTarget ? closeTarget : default;
            return hasTarget;
        }

        private void TryReload()
        {
            foreach (var entity in _reloadRemainingFilter)
            {
                ref var reloadData = ref Componenter.Get<AttackReloadData>(entity);
                reloadData.RemainingTime -= DeltaTime;
                if (reloadData.RemainingTime <= 0) Componenter.Del<AttackReloadData>(entity);
            }
        }
    }
}