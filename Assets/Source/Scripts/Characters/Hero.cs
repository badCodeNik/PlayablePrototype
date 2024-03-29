using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.EventSystem;
using Source.Scripts.LibrariesSystem;
using UnityEngine;
namespace Source.Scripts.Characters
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private HeroChannel heroChannel;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private int entity;

        public HeroInfo HeroInfo => heroInfo;
        public int Entity => entity;

        public void Start()
        {
            EcsInitialize();
            heroChannel.RaiseEvent(this);
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
                movableData.RotationSpeed = heroInfo.Movable.RotationSpeed;
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
            

            if (heroInfo.Attacking.Enabled)
            {
                ref var attackingData = ref componenter.Add<AttackingData>(entity);
                var projectileInfo = Libraries.ProjectileLibrary.GetByID(heroInfo.Attacking.ProjectileID);
                attackingData.AttackSpeed = heroInfo.Attacking.AttackSpeed;//Not reading the value from the inspector
                attackingData.InitializeValues(
                    heroInfo.Attacking.Damage,
                    heroInfo.Attacking.AttackDistance,
                    heroInfo.Attacking.AttackSpeed,
                    projectileInfo.Prefab,
                    projectileInfo.Speed
                    );
            }
            

        }
    }
}