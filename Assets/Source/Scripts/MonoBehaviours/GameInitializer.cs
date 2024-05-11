using System.Collections.Generic;
using Cinemachine;
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

            var heroLibrary = Libraries.HeroPrefabLibrary.GetByID(HeroKeys.Vasya);
            var hero = Resources.Load<GameObject>(heroLibrary.Prefab);
            if (hero != null)
            {
                var vasya = Instantiate(hero);
                _player = vasya;
                _spawnPosition = _player.transform.position;
                virtualCamera.Follow = vasya.transform;
                virtualCamera.LookAt = vasya.transform;
            }

            _currentLocationInstance = Instantiate(_locations[_locationIndex] as GameObject);
            if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
            _locationIndex++;

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
            ClearPreviousLocation();
            if (_locationIndex < _locations.Count)
                _currentLocationInstance = Instantiate(_locations[_locationIndex] as GameObject);
            if (!_usedIndexes.Contains(_locationIndex)) _usedIndexes.Add(_locationIndex);
            _locationIndex++;
            if (_locationIndex == _locations.Count) signal.RegistryRaise(new OnLevelCompletedSignal());
        }


        public void Restart()
        {
            ClearPreviousLocation(); // или DeactivateAllLocations() в зависимости от выбранного подхода
            _locationIndex = 0;
            _usedIndexes.Clear();

            InitializeGame();
            _player.transform.position = _spawnPosition;
        }
    }
}