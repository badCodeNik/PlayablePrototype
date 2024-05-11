using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.Characters;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Ecs.Systems.PerkSystems;
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
            _readyAttackFilter = World.Filter<PlayerMark>().Exc<InputData>().Exc<DestroyingData>().Exc<AttackReloadData>().End();
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

                    if (Componenter.Has<BonusAttackSpeedData>(playerEntity))
                    {
                        ref var bonusAttackSpeedData = ref Componenter.Get<BonusAttackSpeedData>(playerEntity);
                        reloadData.InitializeValues(bonusAttackSpeedData.BonusAttackSpeed);
                    }
                    else reloadData.InitializeValues(attackingData.AttackSpeed);

                    var spawnPosition = playerTransform.position;
                    var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                    CreateProjectile(playerEntity, spawnPosition, angle, targetDirection, attackingData);

                    if (Componenter.Has<ParallelProjectileData>(playerEntity))
                    {
                        ref var parallelData = ref Componenter.Get<ParallelProjectileData>(playerEntity);
                        CreateProjectile(playerEntity, spawnPosition + parallelData.Offset, angle,
                            targetDirection, attackingData);
                    }

                    if (Componenter.Has<BackwardsProjectileData>(playerEntity))
                    {
                        ref var backwardsProjectile = ref Componenter.Get<BackwardsProjectileData>(playerEntity);
                        CreateProjectile(playerEntity, spawnPosition, backwardsProjectile.Angle + angle,
                            targetDirection, attackingData, true);
                    }

                    if (Componenter.Has<SidesProjectileData>(playerEntity))
                    {
                        TryCreateSideProjectiles(playerEntity, targetDirection, spawnPosition, angle, attackingData);
                    }
                }
            }
        }

        private void TryCreateSideProjectiles(int playerEntity, Vector2 targetDirection, Vector3 spawnPosition,
            float angle,
            AttackingData attackingData)
        {
            ref var sidesProjectileData = ref Componenter.Get<SidesProjectileData>(playerEntity);
            var sideAngleOffset = sidesProjectileData.SideAngleOffset;
            Vector2 leftDirection = Quaternion.Euler(0, 0, -sideAngleOffset) * targetDirection;
            Vector2 rightDirection = Quaternion.Euler(0, 0, sideAngleOffset) * targetDirection;
            CreateProjectile(playerEntity, spawnPosition, angle - sideAngleOffset, leftDirection,
                attackingData);
            CreateProjectile(playerEntity, spawnPosition, angle + sideAngleOffset,
                rightDirection, attackingData);
        }

        private void CreateProjectile(int playerEntity, Vector3 position, float angle, Vector2 direction,
            AttackingData attackingData, bool isBackward = false)
        {
            if (isBackward) direction = -direction;
            var newObject =
                Object.Instantiate(attackingData.ProjectilePrefab, position, Quaternion.Euler(0f, 0f, angle));
            var projectile = newObject.GetComponent<Projectile>();
            var velocity = direction * attackingData.ProjectileSpeed;
            var projectileSprite =
                Libraries.ProjectileLibrary.GetByID(ProjectileKeys.PlayerDefault).PreviewSprite;
            projectile.Initialize((targetEntity =>
            {
                RegistryEvent(new OnHitEvent
                {
                    CharacterEntity = playerEntity,
                    TargetEntity = targetEntity
                });
            }), velocity, CharacterFaction.Player, projectileSprite);
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

    public struct ParallelProjectileData : IEcsComponent
    {
        public Vector3 Offset;

        public void InitializeValues(BonusProjectile data)
        {
            Offset = data.Offset;
        }
    }

    public struct BackwardsProjectileData : IEcsComponent
    {
        public float Angle;

        public void InitializeValues(BonusProjectile data)
        {
            Angle = data.Angle;
        }
    }

    public struct SidesProjectileData : IEcsComponent
    {
        public float SideAngleOffset;

        public void InitializeValues(BonusProjectile data)
        {
            SideAngleOffset = data.SideAngleOffset;
        }
    }

    public class BonusProjectile
    {
        public Vector3 Offset;
        public float Angle;
        public float SideAngleOffset;
    }
}