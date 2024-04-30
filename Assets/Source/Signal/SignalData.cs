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

    public struct OnLevelCompletedSignal
    {
    }

    public struct OnRoomCleanedSignal
    {
    }


    public struct OnFreezingAuraChosen
    {
    }


    public struct OnBurningAuraChosen
    {
    }
}