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
                case PerkKeys.BonusHealth:
                    //Componenter.Add<BonusHPPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.HealthRestoration:
                    //Componenter.Add<HPRestorationPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.LifeSteal:
                    Componenter.Add<LifestealPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.BonusDamage:
                    //Componenter.Add<BonusDamagePerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.BonusAttackSpeed:
                   // Componenter.Add<BonusAttackSpeedPerkData>(playerEntity);
                    
                    break;
                case PerkKeys.Bleeding:
                    //Componenter.Add<BleedingPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.CriticalDamage:
                    Componenter.Add<CriticalDamagePerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.BonusProjectileParallel:
                    Componenter.Add<BonusProjectileParallelPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.BonusProjectileBackwards:
                    Componenter.Add<BonusProjectileBackwardsPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.BonusProjectilesSides:
                    Componenter.Add<BonusProjectilesSidesPerkData>(playerEntity);
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    break;
                case PerkKeys.FreezingAura:
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    RegistrySignal(new OnFreezingAuraChosen());
                    break;
                case PerkKeys.BurningAura:
                    Componenter.Del<PerkChoosingMark>(playerEntity);
                    RegistrySignal(new OnBurningAuraChosen());
                    break;
            }
        }
    }
}