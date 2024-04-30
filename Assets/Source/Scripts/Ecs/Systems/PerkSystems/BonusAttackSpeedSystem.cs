using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;

namespace Source.Scripts.Ecs.Systems.PerkSystems
{
    public class BonusAttackSpeedSystem : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (data is { ChosenPerkID: PerkKeys.BonusAttackSpeed, Data : BonusAttackSpeedData attackSpeed } &&
                _playerFilter.TryGetFirstEntity(out int entity))
            {
                ref var attackSpeedData = ref Componenter.AddOrGet<BonusAttackSpeedData>(entity);
                Componenter.Del<PerkChoosingMark>(entity);
                attackSpeed.InitializeValues(attackSpeed);
            }
        }
    }

    public struct BonusAttackSpeedData : IEcsComponent
    {
        public float BonusAttackSpeed;

        public void InitializeValues(BonusAttackSpeedData data)
        {
            BonusAttackSpeed = data.BonusAttackSpeed;
        }
    }
}