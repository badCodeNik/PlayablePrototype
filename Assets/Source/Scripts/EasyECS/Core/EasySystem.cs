
using System;
using System.Linq;
using System.Reflection;
using Source.Scripts.EasyECS.Core;
using Source.SignalSystem;

namespace Source.EasyECS
{
    public abstract class EasySystem : IEcsInitSystem, IEcsRunSystem
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        private EventSystem _eventSystem;
        protected EcsWorld World;
        protected EcsWorld WorldUI;
        protected Componenter Componenter;
        protected Signal Signal;
        
        public void PreInit(GameShare gameShare)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _eventSystem = _gameShare.GetSharedEcsSystem<EventSystem>();
            Componenter = GetSharedEcsSystem<Componenter>();
            Signal = gameShare.Signal;
            InjectFields();
            _isInitialized = true;
        }

        public void RegistrySignal<T>(T data)
        {
            Signal.RegistryRaise(data);
        }

        public void SubscribeSignal<T>(Action<T> action)
        {
            Signal.Subscribe(action);
        }

        public void UnsubscribeSignal<T>(Action<T> action)
        {
            Signal.Unsubscribe(action);
        }

        public void RegistryEvent<T>(T data) where T: struct, IEcsEvent<T>
        {
            _eventSystem.RegistryEvent(data);
        }
        
        public T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            return _gameShare.GetSharedMonoBehaviour<T>();
        }
        
        public T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return _gameShare.GetSharedEcsSystem<T>();
        }

        public void InjectFields()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            foreach (var field in fields)
            {
                var injectAttribute = (EasyInjectAttribute)field.GetCustomAttributes(typeof(EasyInjectAttribute), true).FirstOrDefault();

                if (injectAttribute != null)
                {
                    var fieldType = field.FieldType;

                    if (typeof(EasySystem).IsAssignableFrom(fieldType) && typeof(IEcsSharingSystem).IsAssignableFrom(fieldType))
                    {
                        var sharedEasySystemMethod = typeof(EasySystem).GetMethod("GetSharedEcsSystem").MakeGenericMethod(fieldType);
                        var sharedSystem = sharedEasySystemMethod.Invoke(this, null);

                        field.SetValue(this, sharedSystem);
                    }
                    else if (typeof(EasyMonoBehaviour).IsAssignableFrom(fieldType))
                    {
                        var sharedMonoBehaviourMethod = typeof(EasySystem).GetMethod("GetSharedMonoBehaviour").MakeGenericMethod(fieldType);
                        var sharedMonoBehaviour = sharedMonoBehaviourMethod.Invoke(this, null);

                        field.SetValue(this, sharedMonoBehaviour);
                    }
                    else if (typeof(Configuration).IsAssignableFrom(fieldType))
                    {
                        var configurationHub = GetSharedMonoBehaviour<ConfigurationHub>();

                        var getConfigMethod = typeof(ConfigurationHub).GetMethod("GetConfigByType").MakeGenericMethod(fieldType);
                        var sharedObject = getConfigMethod.Invoke(configurationHub, null);

                        field.SetValue(this, sharedObject);
                    }
                }
            }
        }

        public void Init(IEcsSystems systems)
        {
            World = systems.GetWorld();
            WorldUI = systems.GetWorld("UI");
            Initialize();
        }

        public void Run(IEcsSystems systems)
        {
            Update();
        }
        
        
        
        protected virtual void Initialize(){}
        protected virtual void Update(){}
    }
}