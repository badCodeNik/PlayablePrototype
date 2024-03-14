using Source.Scripts.Data;
using Source.Scripts.EventSystem;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class TestChangeLocalization : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;        
        [SerializeField] private LanguageKeyEventChannel languageKeyEventChannel;        
        
        public void SetRuLocalization()
        {
            if (gameConfiguration.languageID == LanguageKeys.Ru) return;
            gameConfiguration.languageID = LanguageKeys.Ru;
            languageKeyEventChannel.RaiseEvent(gameConfiguration.languageID);
        }
        public void SetEnLocalization()
        {
            if (gameConfiguration.languageID == LanguageKeys.En) return;
            gameConfiguration.languageID = LanguageKeys.En;
            languageKeyEventChannel.RaiseEvent(gameConfiguration.languageID);
        }
    }
}