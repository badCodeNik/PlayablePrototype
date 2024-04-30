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
    public class BleedSystem : EcsEventListener<OnPerkChosen, OnHitEvent>
    {
        private EcsFilter _playerFilter;
        private EcsFilter _bleedingData;

        protected override void Initialize()
        {
            _bleedingData = World.Filter<BleedingData>().End();
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _bleedingData)
            {
                ref var bleedingData = ref Componenter.Get<BleedingData>(entity);
                ref var currentHealth = ref Componenter.Get<DestructableData>(entity).CurrentHealth;
                bleedingData.Timer = bleedingData.TimeRemaining;
                bleedingData.Timer -= Time.fixedDeltaTime;
                var dealingDamage = bleedingData.DamagePerSec * bleedingData.TimeRemaining; 
                if (bleedingData.Timer > 0)
                {
                    currentHealth -= bleedingData.DamagePerSec;
                }
            }
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.Bleeding, Data : BleedData bleed } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var bleedData = ref Componenter.AddOrGet<BleedData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                bleed.InitializeValues(bleed);
            }
        }

        public override void OnEvent(OnHitEvent data)
        {
            if (Componenter.TryGetReadOnly(data.CharacterEntity, out BleedData bleedData))
            {
                ref var bleedingData = ref Componenter.AddOrGet<BleedingData>(data.TargetEntity);
                bleedingData.InitializeValues(bleedData);
            }
        }
    }

    public struct BleedData : IEcsComponent
    {
        public float Time;
        public float Damage;

        public void InitializeValues(float time, float damage)
        {
            Time += time;
            Damage += damage;
        }

        public void InitializeValues(BleedData data)
        {
            Time += data.Time;
            Damage += data.Damage;
        }
    }

    public struct BleedingData : IEcsComponent
    {
        public float TimeRemaining;
        public float DamagePerSec;
        public float Timer;

        public void InitializeValues(BleedData data)
        {
            
        }
    }
}