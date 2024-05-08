using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.KeysHolder;
using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems.PerkSystems
{
    public class AuraSystem : EcsEventListener<OnPerkChosen>
    {
        private EcsFilter _playerFilter;
        private EcsFilter _frozenFilter;

        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
            _frozenFilter = World.Filter<FrozenData>().End();
        }

        protected override void Update()
        {
            foreach (var enemyEntity in _frozenFilter)
            {
                ref var frozenData = ref Componenter.Get<FrozenData>(enemyEntity);
                ref var movableData = ref Componenter.Get<MovableData>(enemyEntity);
                frozenData.TimeRemaining -= DeltaTime;
                if (frozenData.TimeRemaining < 0)
                {
                    movableData.NavMeshAgent.isStopped = true;
                    Componenter.Del<FrozenData>(enemyEntity);
                }
                else
                {
                    movableData.NavMeshAgent.isStopped = false;
                }
            }
        }

        public override void OnEvent(OnPerkChosen data)
        {
            if (_playerFilter.TryGetFirstEntity(out int entity))
            {
                if (data is { ChosenPerkID: PerkKeys.FreezingAura })
                {
                    ref var freezingData = ref Componenter.AddOrGet<FreezingAuraData>(entity);
                    Componenter.Del<PerkChoosingMark>(entity);
                    ref var transform = ref Componenter.Get<TransformData>(entity).Value;
                    freezingData.InitializeValues(EasyNode.GameConfiguration.Perks.FreezingAura);
                    for (int i = 0; i < freezingData.AuraCount; i++)
                    {
                        Vector3 relativePosition =
                            (Quaternion.Euler(0, 0, i * 360f / freezingData.AuraCount) * Vector3.right) *
                            freezingData.OrbitDistance;
                        relativePosition.z = 0; // Убедитесь, что аура находится на плоскости X и Y
                        GameObject auraObject =
                            Object.Instantiate(freezingData.AuraPrefab, transform.position + relativePosition,
                                Quaternion.identity);
                        auraObject.transform.SetParent(transform);
                        Aura auraComponent =
                            auraObject.AddComponent<Aura>(); // Добавьте ваш Aura скрипт здесь, подготовьте свой префаб
                        auraComponent.Initialize((targetEntity =>
                        {
                            RegistryEvent(new OnHitEvent
                            {
                                CharacterEntity = entity,
                                TargetEntity = targetEntity
                            });
                        }), transform, freezingData.RotationSpeed, relativePosition, AuraType.Ice);
                    }
                }

                if (data is { ChosenPerkID: PerkKeys.BurningAura })
                {
                    ref var burningData = ref Componenter.AddOrGet<BurningAuraData>(entity);
                    Componenter.Del<PerkChoosingMark>(entity);
                    ref var transform = ref Componenter.Get<TransformData>(entity).Value;
                    burningData.InitializeValues(EasyNode.GameConfiguration.Perks.BurningAura);
                    for (int i = 0; i < burningData.AuraCount; i++)
                    {
                        Vector3 relativePosition =
                            (Quaternion.Euler(0, 0, i * 360f / burningData.AuraCount) * Vector3.right) *
                            burningData.OrbitDistance;
                        relativePosition.z = 0; // Убедитесь, что аура находится на плоскости X и Y
                        GameObject auraObject =
                            Object.Instantiate(burningData.AuraPrefab, transform.position + relativePosition,
                                Quaternion.identity);
                        auraObject.transform.SetParent(transform);

                        Aura auraComponent =
                            auraObject.AddComponent<Aura>(); // Добавьте ваш Aura скрипт здесь, подготовьте свой префаб
                        auraComponent.Initialize((targetEntity =>
                        {
                            RegistryEvent(new OnHitEvent
                            {
                                CharacterEntity = entity,
                                TargetEntity = targetEntity
                            });
                        }), transform, burningData.RotationSpeed, relativePosition, AuraType.Fire);
                    }
                }
            }
        }
    }

    public class FreezingAura
    {
        public GameObject AuraPrefab;
        public float OrbitDistance;
        public float RotationSpeed;
        public int AuraCount;
    }

    public class BurningAura
    {
        public GameObject AuraPrefab;
        public float OrbitDistance;
        public float RotationSpeed;
        public int AuraCount;
    }

    public struct FreezingAuraData : IEcsComponent
    {
        public GameObject AuraPrefab;
        public float OrbitDistance;
        public float RotationSpeed;
        public int AuraCount;

        public void InitializeValues(FreezingAura data)
        {
            AuraPrefab = data.AuraPrefab;
            OrbitDistance = data.OrbitDistance;
            RotationSpeed = data.RotationSpeed;
            AuraCount = data.AuraCount;
        }
    }

    public struct BurningAuraData : IEcsComponent
    {
        public GameObject AuraPrefab;
        public float OrbitDistance;
        public float RotationSpeed;
        public int AuraCount;

        public void InitializeValues(BurningAura data)
        {
            AuraPrefab = data.AuraPrefab;
            OrbitDistance = data.OrbitDistance;
            RotationSpeed = data.RotationSpeed;
            AuraCount = data.AuraCount;
        }
    }

    

    public struct FrozenData : IEcsComponent
    {
        public float Slowing;
        public float TimeRemaining;

        public void InitializeValues(float value, float duration)
        {
            Slowing = value;
            TimeRemaining = duration;
        }
    }
}