using System.Collections.Generic;
using Cinemachine;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class GameInitializer : MonoSignalListener<OnPerkChosenSignal>
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private List<string> _locationPaths = new List<string>();
        private List<Object> _locations = new List<Object>();
        private List<int> _usedIndexes = new List<int>();
        private GameObject _currentLocationInstance;
        private GameObject _player;
        private Vector3 _spawnPosition;
        private int _locationIndex = 0;
        private Transform _playerSpawnPoint;
        private bool _isInitialized;

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
            HeroKeys selectedCat = DataManager.LoadSelectedCat();
            var heroLibrary = Libraries.HeroPrefabLibrary.GetByID(selectedCat);
            var heroPrefab = Resources.Load<GameObject>(heroLibrary.Prefab);
            
            if (heroPrefab == null) return;
            
            var hero = Instantiate(heroPrefab);
            _player = hero;
            _spawnPosition = _player.transform.position;
            virtualCamera.Follow = hero.transform;
            virtualCamera.LookAt = hero.transform;


            _currentLocationInstance = Instantiate(_locations[_locationIndex] as GameObject);
            if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
            _locationIndex++;
            Debug.Log($"location index is : {_locationIndex}");
            Debug.Log($"number of locations is : {_locations.Count}");

            if (!_isInitialized)
            {
                var hudPrefab = Resources.Load<GameObject>("Prefabs/Hud/Hud");
                if (hudPrefab != null) Instantiate(hudPrefab);

                signal.RegistryRaise(new OnGameInitializedSignal()
                {
                    HeroInfo = heroLibrary.HeroInfo
                });
                _isInitialized = true;
            }
        }

        private void ClearPreviousLocation()
        {
            if (_currentLocationInstance != null)
            {
                Destroy(_currentLocationInstance);
            }
        }


        protected override void OnSignal(OnPerkChosenSignal data)
        {
            if(data.ChosenPerkID == 0) return;
            ClearPreviousLocation();
            if (_locationIndex < _locations.Count)
                _currentLocationInstance = Instantiate(_locations[_locationIndex] as GameObject);
            if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
            _locationIndex++;
            //Подать сигнал о том что последняя локация
            if (_usedIndexes.Count == 10)
            {
                signal.RegistryRaise(new OnLevelCompletedSignal());
                signal.RegistryRaise(new OnHeroKilledSignal());
            }
            
        }


        public void Restart()
        {
            ClearPreviousLocation(); // или DeactivateAllLocations() в зависимости от выбранного подхода
            _locationIndex = 0;
            _usedIndexes.Clear();
            _locationPaths.Clear();
            _locations.Clear();
            InitializeGame();
            _player.transform.position = _spawnPosition;
        }
    }
}