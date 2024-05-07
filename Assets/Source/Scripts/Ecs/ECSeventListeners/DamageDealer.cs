using System;
using System.Collections.Generic;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Ecs.Systems;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Ecs.ECSeventListeners
{
    public class
        DamageDealer : EcsEventListener<OnHitEvent, OnHeroInitializedEvent, OnEnemyInitializedEvent, OnPerkChosen>
    {
        public override void OnEvent(OnHitEvent data)
        {
            //General logic
            ref var targetHealth = ref Componenter.Get<DestructableData>(data.TargetEntity).CurrentHealth;
            ref var playerAttackingData = ref Componenter.Get<AttackingData>(data.CharacterEntity);
            targetHealth -= playerAttackingData.Damage;
            if (targetHealth <= 0)
            {
                Componenter.Add<DestroyingData>(data.TargetEntity).InitializeValues(3);
            }
            
            
            //Critical damage logic
            if (Componenter.Has<CriticalDamageData>(data.CharacterEntity))
            {
                ref var critDamageData = ref Componenter.AddOrGet<CriticalDamageData>(data.CharacterEntity);
                var randomNumber = Random.Range(0, critDamageData.CritChance);
                var isCritAttack = randomNumber == Random.Range(0, critDamageData.CritChance);
                if (isCritAttack)
                {
                    Debug.Log("Crit Attack");
                    var critDamage = playerAttackingData.Damage + critDamageData.CritDamage;
                    targetHealth -= critDamage;
                    
                }
            }
        }

        public override void OnEvent(OnHeroInitializedEvent data)
        {
            var attacking = data.HeroInfo.Attacking;
            if (attacking.Enabled)
            {
                var projectileInfo = Libraries.ProjectileLibrary.GetByID(attacking.ProjectileID);
                ref var attackingData = ref Componenter.Add<AttackingData>(data.Hero.Entity);
                attackingData.InitializeValues(
                    attacking.Damage,
                    attacking.AttackDistance,
                    attacking.AttackSpeed,
                    projectileInfo.Prefab,
                    projectileInfo.Speed,
                    attacking.BaseAttackSpeed
                );
            }
        }


        public override void OnEvent(OnEnemyInitializedEvent data)
        {
            var attacking = data.EnemyInfo.Attacking;
            if (attacking.Enabled)
            {
                ref var attackingData = ref Componenter.Add<AttackingData>(data.Npc.Entity);
                var projectileInfo = Libraries.ProjectileLibrary.GetByID(attacking.ProjectileID);
                attackingData.InitializeValues(
                    attacking.Damage,
                    attacking.AttackDistance,
                    attacking.AttackSpeed,
                    projectileInfo.Prefab,
                    projectileInfo.Speed,
                    attacking.BaseAttackSpeed);
            }
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (World.Filter<PlayerMark>().End().TryGetFirstEntity(out int entity))
            {
                switch (data.ChosenPerkID)
                {
                    case PerkKeys.BonusDamage:
                    {
                        Componenter.Del<PerkChoosingMark>(entity);
                        ref var attackingData = ref Componenter.Get<AttackingData>(entity);
                        attackingData.Damage += EasyNode.GameConfiguration.Perks.BonusDamage.Value;
                        break;
                    }
                    case PerkKeys.CriticalDamage:
                    {
                        Componenter.Del<PerkChoosingMark>(entity);
                        ref var critDamageData = ref Componenter.AddOrGet<CriticalDamageData>(entity);
                        critDamageData.InitializeValues(EasyNode.GameConfiguration.Perks.CriticalDamage);
                        break;
                    }
                    case PerkKeys.BonusProjectileBackwards:
                    {
                        Componenter.Del<PerkChoosingMark>(entity);
                        ref var backwardsProjectileData = ref Componenter.AddOrGet<BackwardsProjectileData>(entity);
                        backwardsProjectileData.InitializeValues(EasyNode.GameConfiguration.Perks.BonusProjectile);
                        break;
                    }
                    case PerkKeys.BonusProjectileParallel:
                    {
                        Componenter.Del<PerkChoosingMark>(entity);
                        ref var parallelProjectileData = ref Componenter.AddOrGet<ParallelProjectileData>(entity);
                        parallelProjectileData.InitializeValues(EasyNode.GameConfiguration.Perks.BonusProjectile);
                        break;
                    }
                    case PerkKeys.BonusProjectilesSides:
                    {
                        Componenter.Del<PerkChoosingMark>(entity);
                        ref var sidesProjectileData = ref Componenter.AddOrGet<SidesProjectileData>(entity);
                        sidesProjectileData.InitializeValues(EasyNode.GameConfiguration.Perks.BonusProjectile);
                        break;
                    }
                }
            }
        }
    }

    [Serializable]
    public class BonusDamage
    {
        public float Value;
    }

    public struct CriticalDamageData : IEcsComponent
    {
        public int CritChance;
        public float CritDamage;

        public void InitializeValues(CriticalDamage data)
        {
            CritChance += data.CritChance;
            CritDamage += data.CritDamage;
        }
    }

    [Serializable]
    public class CriticalDamage
    {
        public int CritChance;
        public float CritDamage;
    }
}