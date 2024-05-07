using System;
using Source.Scripts.Data;
using Source.Scripts.EasyECS.Core;
using Source.SignalSystem;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class EasySystem : IEcsInitSystem, IEcsRunSystem
    {
        protected EasySystem()
        {
        }

        private bool _isInitialized = false;
        private GameShare _gameShare;
        private EventSystem _eventSystem;
        protected EcsWorld World;
        protected Componenter Componenter;
        private Signal _signal;
        private float _deltaTime;
        protected float DeltaTime => _deltaTime;
        protected float TickTime { get; private set; }
        private InitializeType _initializeType;

        public void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _eventSystem = _gameShare.GetSharedObject<EventSystem>();
            Componenter = _gameShare.GetSharedObject<Componenter>();
            _signal = _gameShare.GetSharedObject<Signal>();
            TickTime = tickTime;
            _initializeType = initializeType;
            _deltaTime = GetCurrentTime();
            _isInitialized = true;
        }

        private float GetCurrentTime()
        {
            switch (_initializeType)
            {
                case InitializeType.None:
                    return 0;

                case InitializeType.FixedUpdate:
                    return Time.fixedDeltaTime;

                case InitializeType.Tick:
                    return TickTime;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RegistrySignal<T>(T data)
        {
            _signal.RegistryRaise(data);
        }

        public void SubscribeSignal<T>(Action<T> action)
        {
            _signal.Subscribe(action);
        }

        public void UnsubscribeSignal<T>(Action<T> action)
        {
            _signal.Unsubscribe(action);
        }

        public void RegistryEvent<T>(T data) where T : struct, IEcsEvent<T>
        {
            _eventSystem.RegistryEvent(data);
        }

        public void Init(IEcsSystems systems)
        {
            World = systems.GetWorld();
            Initialize();
        }

        public void Run(IEcsSystems systems)
        {
            Update();
        }


        protected virtual void Initialize()
        {
        }

        protected virtual void Update()
        {
        }

        public virtual InitializeType DefaultInitializeType()
        {
            return InitializeType.None;
        }

        public virtual string Description()
        {
            return "";
        }
    }

    public enum InitializeType
    {
        None,
        Start,
        FixedUpdate,
        Tick
    }
}