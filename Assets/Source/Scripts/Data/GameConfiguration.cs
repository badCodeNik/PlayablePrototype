using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "Data/GameConfiguration")]
    public class GameConfiguration : SerializedScriptableObject, IGameShareItem
    {
        public LanguageKeys languageID;
    }
}