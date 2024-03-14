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
        
        public T GetItemByString<T>(Dictionary<string, T> collection, string id)
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
        SomeHero
    }
    
    public enum EnemyKeys
    {
        [Description("enemy_default_enemy")]
        DefaultEnemy
    }
}