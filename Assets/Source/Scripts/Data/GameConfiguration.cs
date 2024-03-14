using Source.EasyECS;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/GameConfiguration", fileName = "GameConfiguration")]
    public class GameConfiguration : Configuration
    {
        public LanguageKeys languageID;
    }
}