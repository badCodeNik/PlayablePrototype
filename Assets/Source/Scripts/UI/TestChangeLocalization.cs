
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class TestChangeLocalization : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private Signal signal;
        
        public void SetRuLocalization()
        {
            if (gameConfiguration.languageID == LanguageKeys.Ru) return;
            gameConfiguration.languageID = LanguageKeys.Ru;
            signal.RegistryRaise(new OnLanguageChangedSignal { CurrentValue = gameConfiguration.languageID});
        }
        public void SetEnLocalization()
        {
            if (gameConfiguration.languageID == LanguageKeys.En) return;
            gameConfiguration.languageID = LanguageKeys.En;
            signal.RegistryRaise(new OnLanguageChangedSignal { CurrentValue = gameConfiguration.languageID});
        }
    }
}