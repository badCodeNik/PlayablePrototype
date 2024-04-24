using Source.Scripts.Characters;
using Source.Scripts.Data;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.EasyECS.Custom
{
    public struct OnGameStartEvent : IEcsEvent<OnGameStartEvent>
    {
        public int Entity;

        public void InitializeValues(OnGameStartEvent eventData)
        {
            Entity = eventData.Entity;
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

    public struct OnGameInitializedEvent : IEcsEvent<OnGameInitializedEvent>
    {
        public void InitializeValues(OnGameInitializedEvent eventData)
        {
        }
    }

    public struct OnEnemyInitializedEvent : IEcsEvent<OnEnemyInitializedEvent>
    {
        public Npc Npc;
        public EnemyInfo EnemyInfo;

        public void InitializeValues(OnEnemyInitializedEvent eventData)
        {
            Npc = eventData.Npc;
            EnemyInfo = eventData.EnemyInfo;
        }
    }

    public struct OnRoomCleaned : IEcsEvent<OnRoomCleaned>
    {
        public Transform Transform;

        public void InitializeValues(OnRoomCleaned eventData)
        {
            Transform = eventData.Transform;
        }
    }

    public struct OnGetPerk : IEcsEvent<OnGetPerk>
    {
        public int PlayerEntity;

        public void InitializeValues(OnGetPerk eventData)
        {
            PlayerEntity = eventData.PlayerEntity;
        }
    }

    public struct OnPerkChosen : IEcsEvent<OnPerkChosen>
    {
        public int PlayerEntity;
        public PerkKeys ChosenPerkID;

        public void InitializeValues(OnPerkChosen eventData)
        {
            PlayerEntity = eventData.PlayerEntity;
            ChosenPerkID = eventData.ChosenPerkID;
        }
    }

    public struct OnEnemyColliderTouchEvent : IEcsEvent<OnEnemyColliderTouchEvent>
    {
        public int EnemyEntity;
        public int PlayerEntity;

        public void InitializeValues(OnEnemyColliderTouchEvent eventData)
        {
            EnemyEntity = eventData.EnemyEntity;
            PlayerEntity = eventData.PlayerEntity;
        }
    }
}