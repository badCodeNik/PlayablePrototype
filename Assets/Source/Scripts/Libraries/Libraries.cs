using System;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.LibrariesSystem
{
    public class Libraries : MonoBehaviour
    {
        [SerializeField] private HeroLibrary heroLibrary;
        [SerializeField] private EnemiesLibrary enemiesLibrary;
        [SerializeField] private LanguageLibrary languageLibrary;
        private KeyHolder _keysHolder = new();
        private ILibrary[] AllLibraries => new ILibrary[] 
            { 
                heroLibrary, 
                enemiesLibrary,
                languageLibrary
            };

        public static KeyHolder KeysHolder => Instance._keysHolder;
        public static HeroLibrary HeroPrefabLibrary => Instance.heroLibrary;
        public static LanguageLibrary LanguageLibrary => Instance.languageLibrary;
        public static EnemiesLibrary EnemiesLibrary => Instance.enemiesLibrary;

        public static Libraries Instance { get; private set; }

        public Action OnConfigurationsUpdate;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                UpdateConfigurations();
                _keysHolder.Initialize();
                foreach (var library in AllLibraries)
                {
                    library.Initialize();
                }
            }
            else Destroy(this);
        }

        public void UpdateConfigurations()
        {
            OnConfigurationsUpdate?.Invoke();
        }
    }
}