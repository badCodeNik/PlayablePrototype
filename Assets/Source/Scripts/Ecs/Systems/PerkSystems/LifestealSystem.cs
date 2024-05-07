using System;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems.PerkSystems
{
    public class LifestealSystem : EcsEventListener<OnPerkChosen, OnHitEvent>
    {
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.LifeSteal } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var lifestealData = ref Componenter.AddOrGet<LifestealData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                lifestealData.InitializeValues(EasyNode.GameConfiguration.Perks.Lifesteal);
            }
        }

        public override void OnEvent(OnHitEvent data)
        {
            if (Componenter.Has<LifestealData>(data.CharacterEntity))
            {
                ref var lifesteal = ref Componenter.Get<LifestealData>(data.CharacterEntity).Lifesteal;
                ref var attackingData = ref Componenter.Get<AttackingData>(data.CharacterEntity);
                var lifestealValue = (attackingData.Damage / 100) * lifesteal;
                ref var currentHealth = ref Componenter.Get<DestructableData>(data.CharacterEntity).CurrentHealth;
                var maxHealth = Componenter.Get<DestructableData>(data.CharacterEntity).Maxhealth;
                currentHealth += lifestealValue;
                currentHealth = Mathf.Min(currentHealth, maxHealth);
            }
        }
    }

    public struct LifestealData : IEcsComponent
    {
        public float Lifesteal; // percent value

        public void InitializeValues(Lifesteal data)
        {
            Lifesteal += data.LifestealValue;
        }
    }

    [Serializable]
    public class Lifesteal
    {
        public float LifestealValue;
    }
}