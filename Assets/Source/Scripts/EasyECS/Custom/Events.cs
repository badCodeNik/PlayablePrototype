
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.Scripts.EasyECS.Custom
{
    public struct OnGameStartEvent : IEcsEvent<OnGameStartEvent>
    {
        public int entity;
        public void InitializeValues(OnGameStartEvent eventData)
        {
            entity = eventData.entity;
        }
    }
    
    public struct OnProjectileTouch : IEcsEvent<OnProjectileTouch>
    {
        public int CharacterEntity;
        public int TargetEntity;

        public void InitializeValues(OnProjectileTouch eventData)
        {
            CharacterEntity = eventData.CharacterEntity;
            TargetEntity = eventData.TargetEntity;
        }
    }

    public struct OnMoveEvent : IEcsEvent<OnMoveEvent>
    {
        public int Entity;
        public Vector2 Direction;
        public float Speed;
        public void InitializeValues(OnMoveEvent eventData)
        {
            Entity = eventData.Entity;
            Direction = eventData.Direction;
            Speed = eventData.Speed;
        }
    }

    public struct OnEnemyMoveEvent : IEcsEvent<OnEnemyMoveEvent>
    {
        public int Entity;
        public Vector2 Direction;
        public float Speed;
        public void InitializeValues(OnEnemyMoveEvent eventData)
        {
            Entity = eventData.Entity;
            Direction = eventData.Direction;
            Speed = eventData.Speed;
        }
    }
}