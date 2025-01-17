﻿using Source.EasyECS.Interfaces;
using Source.Scripts.Characters;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.SignalSystem
{
    public struct OnLanguageChangedSignal
    {
        public LanguageKeys CurrentValue;
    }

    public struct OnPlayerInitializedSignal
    {
        public Hero Hero;
        public HeroInfo HeroInfo;
    }

    public struct OnEnemyInitializedSignal
    {
        public Npc Npc;
        public EnemyInfo EnemyInfo;
    }

    public struct OnGameInitializedSignal
    {
        public HeroInfo HeroInfo;
    }


    public struct OnLocationCreatedSignal
    {
        public Transform PlayerSpawnPosition;
    }

    public struct OnHitSignal
    {
        public int EnemyEntity;
        public int PlayerEntity;
    }

    public struct OnPerkChosenSignal
    {
        public object Data;
        public PerkKeys ChosenPerkID;
    }

    public struct OnLevelCompletedSignal : IEcsComponent
    {
        
    }

    public struct OnRoomCleanedSignal
    {
    }

    public struct OnEnemyMoveSignal
    {
        public int Entity;
        public Vector2 Direction;
    }

    public struct OnHeroKilledSignal
    {
        public int Entity;
    }

    public struct OnEnemyKilledSignal
    {
        public int Entity;
        public int Coins;
        public int Crystals;
    }

    public struct OnMoneyChangeSignal
    {
        
    }
    
}