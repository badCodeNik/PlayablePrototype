using Source.EasyECS;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;

namespace Source.Scripts.Ecs.Systems
{
    public class KillSystem : EasySystem
    {
        private EcsFilter _enemyDestroyingFilter;
        protected override void Initialize()
        {
            _enemyDestroyingFilter = World.Filter<DestroyingData>().Inc<EnemyMark>().End();
        }

        protected override void Update()
        {
            foreach (var enemyEntity in _enemyDestroyingFilter)
            {
                ref var enemy = ref Componenter.Get<DestructableData>(enemyEntity).Prefab;
                ref var destroyingData = ref Componenter.Get<DestroyingData>(enemyEntity);
                destroyingData.TimeRemaining -= DeltaTime;
                enemy.SetActive(false);
                if (destroyingData.TimeRemaining <= 0)
                {
                    Componenter.DelEntity(enemyEntity);
                    
                }
            }
        }
    }
    
}