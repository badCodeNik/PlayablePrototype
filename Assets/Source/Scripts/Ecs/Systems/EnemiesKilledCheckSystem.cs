using Source.EasyECS;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;

namespace Source.Scripts.Ecs.Systems
{
    public class EnemiesKilledCheckSystem : EasySystem
    {
        private EcsFilter _enemyFilter;
        private EcsFilter _playerFilter;
        private bool _isRoomCleaned;
        protected override void Initialize()
        {
            _enemyFilter = World.Filter<EnemyMark>().End();
            _playerFilter = World.Filter<PlayerMark>().Exc<PerkChoosingMark>().End();
        }

        protected override void Update()
        {
            if (_playerFilter.TryGetFirstEntity(out int playerEntity))
            {
                if (!_enemyFilter.HasAny())
                {
                    ref var playerTransformData = ref Componenter.Get<TransformData>(playerEntity);
                    
                        RegistryEvent(new OnRoomCleaned()
                        {
                            Transform = playerTransformData.Value
                        });
                        
                }
            }
            
                
        }
    }
}