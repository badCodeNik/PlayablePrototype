

using Source.Scripts.Ecs.Systems;

namespace Source.EasyECS
{
    public class EcsStarter : Starter
    {
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems.Add(new InputSystem());
            updateSystems.Add(new HealthChanger());
            updateSystems.Add(new AnimatorListener());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new PlayerMovementSystem());
            fixedUpdateSystems.Add(new PlayerAttackSystem());
            fixedUpdateSystems.Add(new EnemyAttackSystem());
            fixedUpdateSystems.Add(new EnemyMovementSystem());
            fixedUpdateSystems.Add(new KillSystem());
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            tickUpdateSystems.Add(new ProjectileSystem());
        }
    }
}