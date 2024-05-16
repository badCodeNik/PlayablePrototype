using Source.Scripts.Characters;
using Source.Scripts.Data;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.KeysHolder;
using Source.SignalSystem;
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

    public struct OnHitEvent : IEcsEvent<OnHitEvent>
    {
        public int CharacterEntity;
        public int TargetEntity;

        public void InitializeValues(OnHitEvent eventData)
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
        public bool IsMoving;

        public void InitializeValues(OnMoveEvent eventData)
        {
            Entity = eventData.Entity;
            Direction = eventData.Direction;
            Speed = eventData.Speed;
            IsMoving = eventData.IsMoving;
        }
    }

    public struct OnEnemyMoveEvent : IEcsEvent<OnEnemyMoveEvent>
    {
        public int Entity;
        public Vector2 Direction;

        public void InitializeValues(OnEnemyMoveEvent eventData)
        {
            Entity = eventData.Entity;
            Direction = eventData.Direction;
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
        public object Data;
        public PerkKeys ChosenPerkID;

        public void InitializeValues(OnPerkChosen eventData)
        {
            Data = eventData.Data;
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
    
    public struct OnHeroInitializedEvent : IEcsEvent<OnHeroInitializedEvent>
    {
        public Hero Hero;
        public HeroInfo HeroInfo;
        public void InitializeValues(OnHeroInitializedEvent eventData)
        {
            Hero = eventData.Hero;
            HeroInfo = eventData.HeroInfo;
        }
    }
    
    
    public struct OnHeroKilledEvent : IEcsEvent<OnHeroKilledEvent>
    {
        public int Entity;
        public void InitializeValues(OnHeroKilledEvent eventData)
        {
            Entity = eventData.Entity;
        }
    }
    public struct OnEnemyKilledEvent : IEcsEvent<OnEnemyKilledEvent>
    {
        public int Entity;
        public int Coins;
        public int Crystals;
        public void InitializeValues(OnEnemyKilledEvent eventData)
        {
            Entity = eventData.Entity;
            Coins = eventData.Coins;
            Crystals = eventData.Crystals;
        }
    }
    
    public struct OnLevelCompletedEvent : IEcsEvent<OnLevelCompletedEvent>
    {
        public void InitializeValues(OnLevelCompletedEvent eventData)
        {
            
        }
    }
}

