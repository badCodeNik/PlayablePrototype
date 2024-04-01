using Source.Scripts.Characters;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;

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
}