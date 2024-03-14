using System;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.LibrariesSystem
{
    [CreateAssetMenu(menuName = "Library/LanguageLibrary")]
    public class LanguageLibrary : Library<LanguagePack, LanguageKeys>
    {
        
    }

    [Serializable]
    public class LanguagePack : ILibraryItem<LanguageKeys>
    {
        [SerializeField] private LanguageKeys languageID;
        [SerializeField] private WordsPack[] wordsPacks;
        public LanguageKeys ID => languageID;
        public WordsPack[] WordsPacks => wordsPacks;

        public string GetValueByWordID(WordKeys wordID)
        {
            foreach (var wordPack in wordsPacks)
            {
                if (wordPack.WordID == wordID) return wordPack.Value;
            }

            throw new Exception($"Cannot find value by wordID : {wordID}");
        }
    }

    [Serializable]
    public class WordsPack
    {
        [SerializeField] private WordKeys wordID;
        [SerializeField] private string value;

        public WordKeys WordID => wordID;
        public string Value => value;
    }
}