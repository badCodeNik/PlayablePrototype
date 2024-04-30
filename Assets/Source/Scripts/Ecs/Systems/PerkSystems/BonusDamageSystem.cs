using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;

namespace Source.Scripts.Ecs.Systems.PerkSystems
{
    public class BonusDamageSystem : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.BonusDamage, Data : BonusDamageData bonusDamage } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var bonusDamageData = ref Componenter.AddOrGet<BonusDamageData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                bonusDamageData.InitializeValues(bonusDamage);
                ref var attackingData = ref Componenter.Get<AttackingData>(entity);
                attackingData.Damage += bonusDamage.Value;
            }
        }
    }

    public struct BonusDamageData : IEcsComponent
    {
        public float Value;

        public void InitializeValues(BonusDamageData data)
        {
            Value = data.Value;
        }
    }
}