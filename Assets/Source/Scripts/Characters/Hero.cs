using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.ECSeventListeners;
using Source.Scripts.Ecs.Marks;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Characters
{
    public class Hero : MonoSignalListener<OnLocationCreatedSignal>
    {
        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private int entity;
        public HeroInfo HeroInfo => heroInfo;
        public int Entity => entity;

        public void Start()
        {
            EcsInitialize();
            signal.RegistryRaise(new OnPlayerInitializedSignal
            {
                Hero = this,
                HeroInfo = heroInfo
            });
        }

        private void EcsInitialize()
        {
            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            componenter.Add<PlayerMark>(entity);
            ref var transformData = ref componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            ref var lookRotationData = ref componenter.Add<LookRotationData>(entity);
            lookRotationData.InitializeValues(Vector2.zero);
            ref var animatorData = ref componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animator);
            

            // Создаем и прокидываем соотвествующую дату в ECS!
            if (heroInfo.Movable.Enabled)
            {
                ref var movableData = ref componenter.Add<MovableData>(entity);
                movableData.MoveSpeed = heroInfo.Movable.MoveSpeed;
                movableData.CharacterTransform = transform;
            }

            // И все остальные параметры...

            if (heroInfo.Destructable.Enabled)
            {
                ref var destructableData = ref componenter.Add<DestructableData>(entity);
                destructableData.CurrentHealth = heroInfo.Destructable.Health;
                destructableData.Maxhealth = heroInfo.Destructable.MaxHealth;
            }
        }

        protected override void OnSignal(OnLocationCreatedSignal data)
        {
            transform.position = data.PlayerSpawnPosition.position;
        }
    }
}