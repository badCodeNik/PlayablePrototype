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
        [SerializeField] private ProjectileLibrary projectileLibrary;
        [SerializeField] private LocationsLibrary locationsLibrary;
        [SerializeField] private PerksLibrary perksLibrary;


        private KeyHolder _keysHolder = new();

        private ILibrary[] AllLibraries => new ILibrary[] 
            { 
                heroLibrary, 
                enemiesLibrary,
                languageLibrary,
                projectileLibrary,
                locationsLibrary,
                perksLibrary
            };
        
        public PerksLibrary PerksLibrary => perksLibrary;
        public static KeyHolder KeysHolder => Instance._keysHolder;
        public static HeroLibrary HeroPrefabLibrary => Instance.heroLibrary;
        public static LanguageLibrary LanguageLibrary => Instance.languageLibrary;
        public static EnemiesLibrary EnemiesLibrary => Instance.enemiesLibrary;
        public static ProjectileLibrary ProjectileLibrary => Instance.projectileLibrary;
        public LocationsLibrary LocationsLibrary => Instance.locationsLibrary;

        public static Libraries Instance { get; private set; }

        public Action OnConfigurationsUpdate;
        
        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                UpdateConfigurations();
                _keysHolder.Initialize(); foreach (var library in AllLibraries)
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