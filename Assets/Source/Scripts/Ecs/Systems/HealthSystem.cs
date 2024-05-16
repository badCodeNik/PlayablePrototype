using Source.EasyECS;
using Source.Scripts.Ecs.Components;

namespace Source.Scripts.Ecs.Systems
{
    public class HealthSystem : EasySystem
    {
        private EcsFilter _healthFilter;

        protected override void Initialize()
        {
            _healthFilter = World.Filter<DestructableData>().Inc<SliderData>().Exc<DestroyingData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _healthFilter)
            {
                ref var sliderData = ref Componenter.Get<SliderData>(entity);
                ref var currentHealth = ref Componenter.Get<DestructableData>(entity).CurrentHealth;
                ref var maxHealth = ref Componenter.Get<DestructableData>(entity).Maxhealth;
                sliderData.Slider.value = currentHealth;
                sliderData.Slider.maxValue = maxHealth;
                if (currentHealth == 0)
                {
                    Componenter.AddOrGet<DestroyingData>(entity);
                }
            }
        }
    }
}