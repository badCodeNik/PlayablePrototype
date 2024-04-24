using System;
using Source.EasyECS;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components.PerksData;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Ecs.ECSeventListeners
{
    public class PerkListener : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }


        public override void OnEvent(OnPerkChosen data)
        {
            Debug.Log("Ну ты перк выбрал конеш....");
            _playerFilter.TryGetFirstEntity(out int playerEntity);
            switch (data.ChosenPerkID)
            {
                case PerkKeys.BonusHP:
                    Componenter.Add<BonusHPPerkData>(playerEntity);
                    break;
                case PerkKeys.HPRestoration:
                    Componenter.Add<HPRestorationPerkData>(playerEntity);
                    break;
                case PerkKeys.LifeSteal:
                    Componenter.Add<LifestealPerkData>(playerEntity);
                    break;
                case PerkKeys.BonusDamage:
                    Componenter.Add<BonusDamagePerkData>(playerEntity);
                    break;
                case PerkKeys.BonusAttackSpeed:
                    Componenter.Add<BonusAttackSpeedPerkData>(playerEntity);
                    break;
                case PerkKeys.Bleeding:
                    Componenter.Add<BleedingPerkData>(playerEntity);
                    break;
                case PerkKeys.CriticalDamage:
                    Componenter.Add<CriticalDamagePerkData>(playerEntity);
                    break;
                case PerkKeys.BonusProjectileParallel:
                    Componenter.Add<BonusProjectileParallelPerkData>(playerEntity);
                    break;
                case PerkKeys.BonusProjectileBackwards:
                    Componenter.Add<BonusProjectileBackwardsPerkData>(playerEntity);
                    break;
                case PerkKeys.BonusProjectilesSides:
                    Componenter.Add<BonusProjectilesSidesPerkData>(playerEntity);
                    break;
                case PerkKeys.FreezingAura:
                    RegistrySignal(new OnFreezingAuraChosen());
                    break;
                case PerkKeys.BurningAura:
                    RegistrySignal(new OnBurningAuraChosen());
                    break;
            }
        }
    }
}