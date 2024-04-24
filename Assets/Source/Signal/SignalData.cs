using System.Collections.Generic;
using Source.Scripts.Characters;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
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

    public struct OnEnemyColliderTouchSignal
    {
        public int EnemyEntity;
        public int PlayerEntity;
    }

    public struct OnPerkChosenSignal
    {
        public int PlayerEntity;
        public PerkKeys ChosenPerkID;
    }

    public struct OnLevelCompletedSignal
    {
    }

    public struct OnRoomCleanedSignal
    {
    }

    public struct OnPerksGenerated
    {
        public List<PerksPack> PerksPacks;
    }


    public struct OnFreezingAuraChosen
    {
        
    }


    public struct OnBurningAuraChosen
    {
        
    }
}