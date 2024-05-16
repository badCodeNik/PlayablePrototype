using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;

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
                if (currentHealth <= 0) return;
                bleedingData.Timer -= DeltaTime;
                bleedingData.TimeRemaining -= DeltaTime;
                var interval = bleedingData.Interval;

                if (bleedingData.Timer < 0)
                {
                    currentHealth -= bleedingData.DamagePerSec;
                    bleedingData.Timer += interval;
                }

                if (bleedingData.TimeRemaining < 0)
                {
                    Componenter.Del<BleedingData>(entity);
                }
            }
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.Bleeding } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var bleedData = ref Componenter.AddOrGet<BleedData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                bleedData.InitializeValues(EasyNode.GameConfiguration.Perks.Bleed);
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

    public class Bleed
    {
        public float DamageAmount;
        public float Interval;
        public float TimeRemaining;
        public float Timer;
    }

    public struct BleedData : IEcsComponent
    {
        public float Damage;
        public float Interval;
        public float Timer;
        public float TimeRemaining;


        public void InitializeValues(Bleed data)
        {
            Interval += data.Interval;
            TimeRemaining += data.TimeRemaining;
            Damage += data.DamageAmount;
            Timer += data.Timer;
        }
    }

    public struct BleedingData : IEcsComponent
    {
        public float TimeRemaining;
        public float DamagePerSec;
        public float Timer;
        public float Interval;

        public void InitializeValues(BleedData data)
        {
            DamagePerSec += data.Damage;
            TimeRemaining += data.TimeRemaining;
            Interval += data.Interval;
            Timer += data.Timer;
        }
    }
}