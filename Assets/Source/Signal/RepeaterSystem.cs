using Source.EasyECS;
using Source.Scripts.EasyECS.Custom;

namespace Source.SignalSystem
{
    public class RepeaterSystem : EasySystem
    {
        protected override void Initialize()
        {
            SubscribeSignal<OnGameInitializedSignal>(OnGameInitialized);
            SubscribeSignal<OnEnemyInitializedSignal>(OnEnemyInitialized);
            SubscribeSignal<OnEnemyColliderTouchSignal>(OnEnemyColliderTouch);
            SubscribeSignal<OnPerkChosenSignal>(OnPerkChosen);
        }

        private void OnPerkChosen(OnPerkChosenSignal data)
        {
            RegistryEvent(new OnPerkChosen()
            {
                ChosenPerkID = data.ChosenPerkID,
                PlayerEntity = data.PlayerEntity
            });
        }

        private void OnEnemyColliderTouch(OnEnemyColliderTouchSignal data)
        {
            RegistryEvent(new OnEnemyColliderTouchEvent()
            {
                EnemyEntity = data.EnemyEntity,
                PlayerEntity = data.PlayerEntity
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

        protected override void Update()
        {
        }
    }
}