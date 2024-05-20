using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.Ecs.Marks;
using Source.SignalSystem;

namespace Source.Scripts.Ecs.Systems
{
    public class EnemiesKilledCheckSystem : EasySystem
    {
        private EcsFilter _enemyFilter;
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _enemyFilter = World.Filter<EnemyMark>().End();
            _playerFilter = World.Filter<PlayerMark>().Exc<PerkChoosingMark>().End();
        }

        protected override void Update()
        {
            if (!_playerFilter.TryGetFirstEntity(out int playerEntity)) return;

            if (_enemyFilter.HasAny()) return;

            ref var timer = ref Componenter.Get<EnemiesCheckData>(playerEntity).Timer;
            timer -= DeltaTime;

            if (timer <= 0)
            {
                RegistrySignal(new OnRoomCleanedSignal());
                Componenter.Add<PerkChoosingMark>(playerEntity);
                timer = 2;
            }
        }
    }

    public struct EnemiesCheckData : IEcsComponent
    {
        public float Timer;
    }
}