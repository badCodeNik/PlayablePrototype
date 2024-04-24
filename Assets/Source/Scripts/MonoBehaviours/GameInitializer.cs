using System.Collections.Generic;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class GameInitializer : MonoSignalListener<OnPerkChosenSignal>
    {
        [SerializeField] private GameObject loadingBanner;
        private List<string> _locationPaths = new List<string>();
        private List<Object> _locations = new List<Object>();
        private List<int> _usedIndexes = new List<int>();
        
        private int _locationIndex = 0;
        private Transform _playerSpawnPoint;
       
        
        private void Start()
        { 
            InitializeGame();
        }

        private void InitializeGame()
        {
            
                var locationPack = Libraries.Instance.LocationsLibrary.GetAllPacks();

                foreach (var location in locationPack)
                {
                    _locationPaths.Add(location.PrefabPath);
                }

                foreach (var locationPath in _locationPaths)
                {
                    _locations.Add(Resources.Load(locationPath));
                }

                var hudPrefab = Resources.Load<GameObject>("Prefabs/Hud/Hud");
                if (hudPrefab != null) Instantiate(hudPrefab);

                Instantiate(_locations[_locationIndex]);
                if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
                _locationIndex++;
                
                 var heroLibrary = Libraries.HeroPrefabLibrary.GetByID(HeroKeys.Vasya);
                 var hero = Resources.Load<GameObject>(heroLibrary.Prefab);
                 if (hero != null) Instantiate(hero);
                 signal.RegistryRaise(new OnGameInitializedSignal()
                 {
                     HeroInfo = heroLibrary.HeroInfo
                 });
            
            
        }
        

        protected override void OnSignal(OnPerkChosenSignal data)
        {
            if (_locationIndex < _locations.Count) Instantiate(_locations[_locationIndex]);
            if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
            _locationIndex++;
            if (_locationIndex == _locations.Count) signal.RegistryRaise(new OnLevelCompletedSignal());
            
        }
    }
}