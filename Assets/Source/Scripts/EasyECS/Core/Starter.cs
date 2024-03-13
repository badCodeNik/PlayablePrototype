
using System.Collections.Generic;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class Starter : EasyMonoBehaviour, IEasyUpdate, IEasyFixedUpdate, IEasyLateUpdate
    {
        [SerializeField] private float tickTime = 1f;
        [SerializeField] private List<string> _bootQueue;
        
        private GameShare _gameSharing;
        private EcsWorld _world;
        private EventSystem _eventSystem;
        private IEcsSystems _stepByStepSystems;
        private IEcsSystems _cardViewRefreshSystems;
        private IEcsSystems _coreSystems;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _lateUpdateSystems;
        private IEcsSystems _tickUpdateSystems;
        private float _tickTimer;
        
        public void SetSharedData(GameShare gameShare)
        {
            _gameSharing = gameShare;
        }

        public void PreInit()
        {
            _world = new EcsWorld();
            _eventSystem = new EventSystem();
            PrepareCoreSystems();
            PrepareInitSystems();
            PrepareUpdateSystems();
            PrepareFixedUpdateSystems();
            PrepareLateUpdateSystems();
            PrepareTickUpdateSystems();
            DependencyInject();
            _eventSystem.PreInit(_gameSharing);
            InitEvents();
        }

        private void DependencyInject()
        {
            InjectSystems(_coreSystems);
            InjectSystems(_initSystems);
            InjectSystems(_updateSystems);
            InjectSystems(_fixedUpdateSystems);
            InjectSystems(_lateUpdateSystems);
            InjectSystems(_tickUpdateSystems);
        }

        private void InitEvents()
        {
            InitEventSubscribes(_coreSystems);
            InitEventSubscribes(_initSystems);
            InitEventSubscribes(_updateSystems);
            InitEventSubscribes(_fixedUpdateSystems);
            InitEventSubscribes(_lateUpdateSystems);
            InitEventSubscribes(_tickUpdateSystems);
        }

        private void InitEventSubscribes(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EcsEventListener listener)
                {
                    _eventSystem.AddListener(listener);
                }
            }
        }
        
        private void InjectSystems(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem easySystem)
                {
                    easySystem.PreInit(_gameSharing);
                }
            }
        }
        
        public override void Initialize()
        {
            InitBootInfo();
            
            _coreSystems.Init();
            _initSystems.Init();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
            _lateUpdateSystems.Init();
            _tickUpdateSystems.Init();
 
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetUpdateSystems(IEcsSystems updateSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetLateUpdateSystems(IEcsSystems lateUpdateSystems);
        protected abstract void SetTickUpdateSystems(IEcsSystems tickUpdateSystems);

        private void InitBootInfo()
        {
            _bootQueue = new List<string>();
            AddToBoot(_coreSystems);
            AddToBoot(_initSystems);
            AddToBoot(_updateSystems);
            AddToBoot(_fixedUpdateSystems);
            AddToBoot(_lateUpdateSystems);
            AddToBoot(_tickUpdateSystems);
        }

        private void AddToBoot(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                _bootQueue.Add(system.GetType().Name);
            }
        }
        
        private void AddToShare(IEcsSystems systems)
        {
            var sharingSystems = systems.GetAllSharingSystems();
            if (sharingSystems.Count < 1) return;
            foreach (var sharingSystem in sharingSystems)
            {
                var type = sharingSystem.GetType();
                _gameSharing.AddSharedEcsSystem(type, sharingSystem);
            }
        }
 

        private void TryInvokeTick()
        {
            _tickTimer += Time.fixedDeltaTime;
            if (!(_tickTimer >= tickTime)) return;
            _tickTimer -= tickTime;
            _tickUpdateSystems?.Run();
        }
        
        private void PrepareCoreSystems()
        {
            _coreSystems = new EcsSystems(_world, _gameSharing);
            _coreSystems.Add(new Componenter());
            _coreSystems.Inject();
            AddToShare(_coreSystems);
        }
        
        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, _gameSharing);
            SetInitSystems(_initSystems);
            _initSystems.Inject();
            AddToShare(_initSystems);
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, _gameSharing);
            SetUpdateSystems(_updateSystems);
            _updateSystems.Inject();
            AddToShare(_updateSystems);
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, _gameSharing);
            _fixedUpdateSystems.Add(_eventSystem);
            SetFixedUpdateSystems(_fixedUpdateSystems);
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                _fixedUpdateSystems.Add(new UnityEditor.EcsWorldDebugSystem());
#endif
            
            _fixedUpdateSystems.Inject();
            AddToShare(_fixedUpdateSystems);
        }
        
        private void PrepareLateUpdateSystems()
        {
            _lateUpdateSystems = new EcsSystems(_world, _gameSharing);
            SetLateUpdateSystems(_lateUpdateSystems);
            _lateUpdateSystems.Inject();
            AddToShare(_lateUpdateSystems);
        }
        
        private void PrepareTickUpdateSystems()
        {
            _tickUpdateSystems = new EcsSystems(_world, _gameSharing);
            SetTickUpdateSystems(_tickUpdateSystems);
            _tickUpdateSystems.Inject();
            AddToShare(_tickUpdateSystems);
        }
        
        private void OnDestroy() 
        {
            _coreSystems?.Destroy();
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
            _tickUpdateSystems?.Destroy();
        }

        public void EasyUpdate()
        {
            _updateSystems?.Run();
        }

        public void EasyFixedUpdate()
        {
            _fixedUpdateSystems?.Run();
            TryInvokeTick();
        }

        public void EasyLateUpdate()
        {
            _lateUpdateSystems?.Run();
        }
    }
}
