using DG.Tweening;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Source.Scripts.Characters
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField, ReadOnly] private int entity;
        [SerializeField] private Signal signal;
        private Slider _slider;
        private Tween _tween;

        public EnemyInfo EnemyInfo => enemyInfo;
        public int Entity => entity;

        public void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            _tween = spriteRenderer.DOFade(1, 1);
            _slider = GetComponentInChildren<Slider>();
            EcsInitialize();
        }


        private void EcsInitialize()
        {
            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            componenter.Add<EnemyMark>(entity);
            ref var transformData = ref componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            ref var animatorData = ref componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animator);
            signal.RegistryRaise(new OnEnemyInitializedSignal
            {
                Npc = this,
                EnemyInfo = EnemyInfo
            });
            agent.updateRotation = false;
            agent.updateUpAxis = false;


            // Создаем и прокидываем соотвествующую дату в ECS!
            if (EnemyInfo.Movable.Enabled)
            {
                ref var movableData = ref componenter.Add<MovableData>(entity);
                movableData.InitializeValue(EnemyInfo.Movable.MoveSpeed, transform,
                    agent);
            }

            // И все остальные параметры...


            if (EnemyInfo.Destructable.Enabled)
            {
                ref var destructableData = ref componenter.AddOrGet<DestructableData>(entity);
                destructableData.InitializeValues(
                    enemyInfo.Destructable.MaxHealth,
                    enemyInfo.Destructable.Health,
                    gameObject,
                    enemyInfo.Destructable.CoinsForKill,
                    enemyInfo.Destructable.CrystalsForKill
                );

                if (_slider == null) return;
                
                ref var sliderData = ref componenter.Add<SliderData>(entity);
                sliderData.Slider = _slider;
                _slider.maxValue = destructableData.Maxhealth;
                _slider.value = destructableData.CurrentHealth;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                Debug.Log("Kek");
                signal.RegistryRaise(new OnHitSignal()
                {
                    EnemyEntity = entity,
                    PlayerEntity = hero.Entity
                });
            }
        }
    }
}