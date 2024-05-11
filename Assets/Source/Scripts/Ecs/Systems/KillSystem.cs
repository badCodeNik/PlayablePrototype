using Source.EasyECS;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class KillSystem : EcsEventListener<OnHeroKilledEvent, OnEnemyKilledEvent>
    {
        private EcsFilter _enemyDestroyingFilter;
        private EcsFilter _playerDestroyingFilter;
        private EcsFilter _enemyFilter;

        protected override void Initialize()
        {
            _enemyDestroyingFilter = World.Filter<DestroyingData>().Inc<EnemyMark>().End();
            _playerDestroyingFilter = World.Filter<DestroyingData>().Inc<PlayerMark>().End();
            _enemyFilter = World.Filter<EnemyMark>().End();
        }

        protected override void Update()
        {
            foreach (var enemyEntity in _enemyDestroyingFilter)
            {
                ref var enemy = ref Componenter.Get<DestructableData>(enemyEntity).Prefab;
                ref var destroyingData = ref Componenter.Get<DestroyingData>(enemyEntity);
                var destructableData = Componenter.Get<DestructableData>(enemyEntity);
                destroyingData.TimeRemaining -= DeltaTime;
                enemy.SetActive(false);
                if (destroyingData.TimeRemaining <= 0)
                {
                    Componenter.DelEntity(enemyEntity);
                    RegistryEvent(new OnEnemyKilledEvent()
                    {
                        Coins = destructableData.CoinsForKill,
                        Crystals = destructableData.CrystalsForKill,
                        Entity = enemyEntity
                    });
                }
            }

            TryKillPlayer();
        }

        private void TryKillPlayer()
        {
            foreach (var player in _playerDestroyingFilter)
            {
                ref var destroyingData = ref Componenter.Get<DestroyingData>(player);
                destroyingData.TimeRemaining -= DeltaTime;
                if (destroyingData.TimeRemaining <= 0)
                {
                    RegistrySignal(new OnHeroKilledSignal()
                    {
                        Entity = player
                    });
                    Componenter.Del<DestroyingData>(player);
                    ref var destructableData = ref Componenter.Get<DestructableData>(player);
                    destructableData.CurrentHealth = destructableData.Maxhealth;
                }
            }
        }

        public override void OnEvent(OnHeroKilledEvent data)
        {
            Componenter.DelEntity(data.Entity);
            foreach (var enemy in _enemyFilter)
            {
                Componenter.Add<DestroyingData>(enemy);
            }
        }

        public override void OnEvent(OnEnemyKilledEvent data)
        {
            MoneyManager.SaveCoins(data.Coins);
            Debug.Log(MoneyManager.LoadCoins());
            MoneyManager.SaveCrystals(data.Crystals);
            Debug.Log(MoneyManager.LoadCrystals());
        }
    }
}