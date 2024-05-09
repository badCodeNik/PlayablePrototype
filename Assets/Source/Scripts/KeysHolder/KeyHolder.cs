using System;
using System.Collections.Generic;
using System.ComponentModel;
using Source.Scripts.Extensions;

namespace Source.Scripts.KeysHolder
{
    public class KeyHolder
    {
        private Dictionary<string, HeroKeys> _heroKeys = new();
        private Dictionary<string, EnemyKeys> _enemyKeys = new();
        private Dictionary<string, PerkKeys> _perkKeys = new();

        public PerkKeys GetPerkKeyByID(string id) => GetItemByString(_perkKeys, id);
        public HeroKeys GetHeroKeyByID(string id) => GetItemByString(_heroKeys, id);
        public EnemyKeys GetEnemyKeyByID(string id) => GetItemByString(_enemyKeys, id);
        
        private T GetItemByString<T>(Dictionary<string, T> collection, string id)
        {
            return collection[id];
        }

        public void InitEnum<T>(Dictionary<string, T> collection) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                collection[value.GetDescription()] = value;
            }
        }

        public void Initialize()
        {
            InitEnum(_heroKeys);
            InitEnum(_enemyKeys);
        }
    }
    
    public enum HeroKeys
    {
        [Description("hero_some_hero")]
        SomeHero,
        [Description("hero_vasya")]
        Vasya
    }
    
    public enum EnemyKeys
    {
        [Description("npc_range_enemy")]
        RangeEnemy,
        [Description("npc_melee_enemy")]
        MeleeEnemy,
        [Description("npc_fly_enemy")]
        FlyEnemy,
        [Description("npc_ghost")]
        Ghost
    }
    
    public enum LocationKeys
    {
        [Description("start_location")]
        StartLocation,
        [Description("second_location")]
        SecondLocation,
        [Description("third_location")]
        ThirdLocation,
        [Description("fourth_location")]
        FourthLocation,
        [Description("fifth_location")]
        FifthLocation,
        [Description("sixth_location")]
        SixthLocation,
        [Description("seventh_location")]
        SeventhLocation,
        [Description("eighth_location")]
        EighthLocation,
        [Description("ninth_location")]
        NinthLocation,
        [Description("Tenth_location")]
        TenthLocation
    }
    
    public enum ProjectileKeys
    {
        [Description("projectile_player_default")]
        PlayerDefault,
        [Description("projectile_enemy_default")]
        EnemyDefault,
        
    }

    public enum LanguageKeys
    {
        [Description("ru")]
        Ru,
        [Description("en")]
        En
    }

    public enum WordKeys
    {
        [Description("MainMenu_Play_Btn")]
        MainMenuPlayBtn,
        [Description("MainMenu_Raider_Btn")]
        MainMenuRaiderBtn,
        [Description("MainMenu_Shop_Btn")]
        MainMenuShopBtn
    }
    
    
    
    public enum PerkKeys
    {
        [Description("BonusHealth")]
        BonusHealth,
        [Description("HealthRestoration")]
        HealthRestoration,
        [Description("Lifesteal")]
        LifeSteal,
        [Description("BonusDamage")]
        BonusDamage,
        [Description("BonusAttackSpeed")]
        BonusAttackSpeed,
        [Description("Bleeding")]
        Bleeding,
        [Description("CriticalDamage")]
        CriticalDamage,
        [Description("BonusProjectileParallel")]
        BonusProjectileParallel,
        [Description("BonusProjectileBackwards")]
        BonusProjectileBackwards,
        [Description("BonusProjectilesSides")]
        BonusProjectilesSides,
        [Description("FreezingAura")]
        FreezingAura,
        [Description("BurningAura")]
        BurningAura
    }
    
    
}