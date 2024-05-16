using Source.EasyECS;
using Source.Scripts.EasyECS.Custom;

namespace Source.SignalSystem
{
    public class RepeaterSystem : EasySystem
    {
        protected override void Initialize()
        {
            SubscribeSignal<OnGameInitializedSignal>(OnGameInitialized);
            SubscribeSignal<OnPlayerInitializedSignal>(OnHeroInitialized);
            SubscribeSignal<OnEnemyInitializedSignal>(OnEnemyInitialized);
            SubscribeSignal<OnHitSignal>(OnHit);
            SubscribeSignal<OnPerkChosenSignal>(OnPerkChosen);
            SubscribeSignal<OnEnemyMoveSignal>(OnEnemyMove);
            SubscribeSignal<OnHeroKilledSignal>(OnHeroKilled);
            SubscribeSignal<OnLevelCompletedSignal>(OnLevelCompleted);
        }

        private void OnLevelCompleted(OnLevelCompletedSignal data)
        {
            RegistryEvent(new OnLevelCompletedEvent()
            {
            });
        }

        private void OnHeroKilled(OnHeroKilledSignal data)
        {
            RegistryEvent(new OnHeroKilledEvent
            {
                Entity = data.Entity
            });
        }

        private void OnEnemyMove(OnEnemyMoveSignal data)
        {
            RegistryEvent(new OnEnemyMoveEvent()
            {
                Direction = data.Direction,
                Entity = data.Entity
            });
        }

        private void OnHeroInitialized(OnPlayerInitializedSignal data)
        {
            RegistryEvent(new OnHeroInitializedEvent()
            {
                Hero = data.Hero,
                HeroInfo = data.HeroInfo
            });
        }

        private void OnPerkChosen(OnPerkChosenSignal data)
        {
            RegistryEvent(new OnPerkChosen()
            {
                ChosenPerkID = data.ChosenPerkID,
                Data = data.Data
            });
        }

        private void OnHit(OnHitSignal data)
        {
            RegistryEvent(new OnHitEvent()
            {
                CharacterEntity = data.EnemyEntity,
                TargetEntity = data.PlayerEntity
            });
        }

        private void OnEnemyInitialized(OnEnemyInitializedSignal data)
        {
            RegistryEvent(new OnEnemyInitializedEvent()
            {
                Npc = data.Npc,
                EnemyInfo = data.EnemyInfo
            });
        }

        private void OnGameInitialized(OnGameInitializedSignal data)
        {
            RegistryEvent(new OnGameInitializedEvent()
            {
            });
        }
    }
}