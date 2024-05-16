using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.Ecs.Systems;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Characters
{
    public class Hero : MonoSignalListener<OnLocationCreatedSignal, OnHeroKilledSignal>
    {
        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private int entity;
        public HeroInfo HeroInfo => heroInfo;
        private float _checkLevelForEnemies = 2f;
        private Componenter _componenter;
        public int Entity => entity;
        private Slider _slider;

        public void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            EcsInitialize();
            signal.RegistryRaise(new OnPlayerInitializedSignal
            {
                Hero = this,
                HeroInfo = heroInfo
            });
        }

        private void EcsInitialize()
        {
            _componenter = EasyNode.EcsComponenter;
            entity = _componenter.GetNewEntity();
            _componenter.Add<PlayerMark>(entity);
            ref var transformData = ref _componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            ref var lookRotationData = ref _componenter.Add<LookRotationData>(entity);
            lookRotationData.InitializeValues(Vector2.zero);
            ref var animatorData = ref _componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animator);
            ref var enemyChecker = ref _componenter.Add<EnemiesCheckData>(entity);
            enemyChecker.Timer = _checkLevelForEnemies;
            
            

            // Создаем и прокидываем соотвествующую дату в ECS!
            if (heroInfo.Movable.Enabled)
            {
                ref var movableData = ref _componenter.Add<MovableData>(entity);
                movableData.MoveSpeed = heroInfo.Movable.MoveSpeed;
                movableData.CharacterTransform = transform;
            }

            // И все остальные параметры...

            if (heroInfo.Destructable.Enabled)
            {
                ref var destructableData = ref _componenter.Add<DestructableData>(entity);
                destructableData.CurrentHealth = heroInfo.Destructable.Health;
                destructableData.Maxhealth = heroInfo.Destructable.MaxHealth;

                if (_slider == null) return;

                ref var sliderData = ref _componenter.Add<SliderData>(entity);
                sliderData.Slider = _slider;
                _slider.maxValue = destructableData.Maxhealth;
                _slider.value = destructableData.CurrentHealth;
            }
        }

        protected override void OnSignal(OnLocationCreatedSignal data)
        {
            transform.position = data.PlayerSpawnPosition.position;
        }

        protected override void OnSignal(OnHeroKilledSignal data)
        {
            _componenter.Add<DestroyingData>(data.Entity);
            Destroy(gameObject, 0.1f);
        }
        
    }
}