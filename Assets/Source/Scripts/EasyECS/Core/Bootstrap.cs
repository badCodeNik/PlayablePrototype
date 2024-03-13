using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.EasyECS
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private List<EasyMonoBehaviour> _awake = new List<EasyMonoBehaviour>();
        [SerializeField] private List<EasyMonoBehaviour> _start = new List<EasyMonoBehaviour>();
        [SerializeField] private List<EasyMonoBehaviour> _share = new List<EasyMonoBehaviour>();

        [SerializeField] private List<DataPack> bootQueue;
        private Dictionary<Type, DataPack> _sharedData;
        [SerializeField] private GameShare gameShare;
        [SerializeField, HideInInspector] private bool _isInitialized = false;
        
        private Action _onUpdate;
        private Action _onFixedUpdate;
        private Action _onLateUpdate;
        
        private void PreInit(EasyMonoBehaviour easyMonoBeh)
        {
            if (!_sharedData.ContainsKey(easyMonoBeh.GetType()))
            {
                var newPack = new DataPack(easyMonoBeh.GetType(), easyMonoBeh);
                easyMonoBeh.PreInit(gameShare, newPack);
                _sharedData[easyMonoBeh.GetType()] = newPack;
                
                if (easyMonoBeh is Starter ecsStarter)
                {
                    ecsStarter.SetSharedData(gameShare);
                    ecsStarter.PreInit();
                }
                if (easyMonoBeh is ConfigurationHub configurationHub)
                {
                    configurationHub.PreInitialize();
                }

                if (easyMonoBeh is IEasyUpdate easyUpdate) _onUpdate += easyUpdate.EasyUpdate;
                if (easyMonoBeh is IEasyFixedUpdate easyFixedUpdate) _onFixedUpdate += easyFixedUpdate.EasyFixedUpdate;
                if (easyMonoBeh is IEasyLateUpdate easyLateUpdate) _onLateUpdate += easyLateUpdate.EasyLateUpdate;
            }
            
        }

        private void PreInitAll()
        {
            _sharedData = new Dictionary<Type, DataPack>();
            bootQueue = new List<DataPack>();
            gameShare = new GameShare(_sharedData);
                        
            foreach (var monoBeh in _share)
            {
                PreInit(monoBeh);
            }
            
            foreach (var monoBeh in _awake)
            {
                PreInit(monoBeh);
            }
            
            foreach (var monoBeh in _start)
            {
                PreInit(monoBeh);
            }

            foreach (var easyMonoBehaviour in _share) InjectDependencies(easyMonoBehaviour);
            foreach (var easyMonoBehaviour in _awake) InjectDependencies(easyMonoBehaviour);
            foreach (var easyMonoBehaviour in _start) InjectDependencies(easyMonoBehaviour);
            
        }
        
        private void InjectDependencies(EasyMonoBehaviour easyMonoBehaviour)
        {
            easyMonoBehaviour.Inject();
        }
        
        private void Awake()
        {
            PreInitAll();
            
            foreach (var initMonoBeh in _awake)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
            
            foreach (var initMonoBeh in _start)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            _onLateUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            _onUpdate = null;
            _onFixedUpdate = null;
            _onLateUpdate = null;
        }

        private void OnValidate()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            gameObject.name = "Bootstrapper";
            TryCreateElement<ConfigurationHub>(_share);
            TryCreateElement<EasyNode>(_awake);
            TryCreateElement<EcsStarter>(_start);
        }

        private void TryCreateElement<T>(List<EasyMonoBehaviour> elementList) where T : EasyMonoBehaviour
        {
            
            if (GetIsAnyOfType<T>()) return;
            
            var elementObject = new GameObject
            {
                transform =
                {
                    parent = transform
                }
            };

            var element = elementObject.AddComponent<T>();
            elementObject.name = element.GetType().Name;
            elementList.Add(element);
        }

        private bool GetIsAnyOfType<T>()
        {
            var inAwake = _awake != null && _awake.OfType<T>().Any();
            var inStart = _start != null && _start.OfType<T>().Any();
            var inShare = _share != null && _share.OfType<T>().Any();
            return inAwake || inStart || inShare;
        }
        
        private T GetCurrentEasyObject<T>() where T : EasyMonoBehaviour
        {
            foreach (var easyMonoBehaviour in _share)
            {
                if (easyMonoBehaviour is T typeEasyMonoBeh) return typeEasyMonoBeh;
            }
            foreach (var easyMonoBehaviour in _awake)
            {
                if (easyMonoBehaviour is T typeEasyMonoBeh) return typeEasyMonoBeh;
            }
            foreach (var easyMonoBehaviour in _start)
            {
                if (easyMonoBehaviour is T typeEasyMonoBeh) return typeEasyMonoBeh;
            }

            throw new Exception($"Can't find {typeof(T)}");
        }
        
#if UNITY_EDITOR
        [Button(ButtonSizes.Medium)]
        public void UpdateAllSettings()
        {
            GetCurrentEasyObject<ConfigurationHub>().GetAllData();
            for (int i = _share.Count - 1; i >= 0; i--) if (_share[i] == null) _share.RemoveAt(i);
            for (int i = _awake.Count - 1; i >= 0; i--) if (_awake[i] == null) _awake.RemoveAt(i);
            for (int i = _start.Count - 1; i >= 0; i--) if (_start[i] == null) _start.RemoveAt(i);
        }

#endif
    }
    
    public class EasyInjectAttribute : PropertyAttribute { }
}