using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;

namespace Source.Scripts.Ecs.Systems.PerkSystems
{
    public class BonusHealthSystem : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.BonusHealth} &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var bonusHealthData = ref Componenter.AddOrGet<BonusHealthData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                bonusHealthData.InitializeValues(EasyNode.GameConfiguration.Perks.BonusHealth);
                ref var destructableData = ref Componenter.Get<DestructableData>(entity);
                destructableData.CurrentHealth += bonusHealthData.BonusHealth;
            }
        }
    }


    public class BonusHealth
    {
        public float Health;
    }
    public struct BonusHealthData : IEcsComponent
    {
        public float BonusHealth;

        public void InitializeValues(BonusHealth data)
        {
            BonusHealth += data.Health;
        }
    }
}