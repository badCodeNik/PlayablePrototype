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
    public class HealthRestorationSystem : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;
        private EcsFilter _healthRestorationDataFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
            _healthRestorationDataFilter = World.Filter<HealthRestorationData>().End();
        }

        protected override void Update()
        {
            TryHeal();
        }

        private void TryHeal()
        {
            foreach (var playerEntity in _healthRestorationDataFilter)
            {
                ref var healthRestorationData = ref Componenter.Get<HealthRestorationData>(playerEntity);
                ref var destructableData = ref Componenter.Get<DestructableData>(playerEntity);

                healthRestorationData.Timer -= DeltaTime;
                var interval = healthRestorationData.Interval;

                if (healthRestorationData.Timer <= 0f)
                {
                    ref var currentHealth = ref Componenter.Get<DestructableData>(playerEntity).CurrentHealth;
                    var maxHealth = Componenter.Get<DestructableData>(playerEntity).Maxhealth;
                    currentHealth += healthRestorationData.RestorationAmount;
                    currentHealth = Mathf.Min(currentHealth, maxHealth);
                    healthRestorationData.Timer += interval;
                }
            }
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.HealthRestoration } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var healthRestorationData = ref Componenter.AddOrGet<HealthRestorationData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                healthRestorationData.InitializeValues(EasyNode.GameConfiguration.Perks.HealthRestoration);
            }
        }
    }


    public class HealthRestoration
    {
        public float RestorationAmount;
        public float Interval;
        public float Timer;
    }

    public struct HealthRestorationData : IEcsComponent
    {
        public float RestorationAmount;
        public float Interval;
        public float Timer;
        public float TimeRemaining;

        public void InitializeValues(HealthRestoration data)
        {
            RestorationAmount += data.RestorationAmount;
            Interval += data.Interval;
        }
    }
}