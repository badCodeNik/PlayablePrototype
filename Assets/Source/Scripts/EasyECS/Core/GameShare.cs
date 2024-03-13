using System;
using System.Collections.Generic;
using UnityEngine;


namespace Source.EasyECS
{
    [Serializable]
    public class GameShare
    {        
        public GameShare(Dictionary<Type, DataPack> monoBehShared)
        {
            _monoBehShared = monoBehShared;
            _ecsShared = new Dictionary<Type, IEcsSharingSystem>();
            sharedEcsSystems = new List<string>();
        }
        
        [SerializeField] private List<string> sharedEcsSystems;
        private Dictionary<Type, DataPack> _monoBehShared;
        private Dictionary<Type, IEcsSharingSystem> _ecsShared;

        public Dictionary<Type, DataPack> MonoBehShared => _monoBehShared;
        public Dictionary<Type, IEcsSharingSystem> EcsShared => _ecsShared;
        
        public T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            var classPack = _monoBehShared[typeof(T)];
            var monoBeh = classPack.MonoBehaviour;
            return (T)monoBeh;
        }

        public void AddSharedEcsSystem<T>(Type type, T ecsSystem) where T : IEcsSharingSystem
        {
            sharedEcsSystems.Add(type.Name);
            _ecsShared[type] = ecsSystem;
        }
        
        public T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return (T)_ecsShared[typeof(T)];
        }
    }
}
