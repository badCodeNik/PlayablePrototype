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
        [Description("enemy_range_enemy")]
        RangeEnemy,
        [Description("enemy_melee_enemy")]
        MeleeEnemy
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
    
    
}