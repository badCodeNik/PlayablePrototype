using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.SignalSystem
{
    [CreateAssetMenu(menuName = "SignalSystem")]
    public class Signal : ScriptableObject
    {
        private Dictionary<Type, List<object>> _actions = new Dictionary<Type, List<object>>();

        public void RegistryRaise<T>(T data)
        {
            var type = typeof(T);
            TryCreateIfNotExist(type);
            var actions = _actions[type];

            foreach (var action in actions)
            {
                var actionTyped = action as Action<T>;
                actionTyped?.Invoke(data);
            }
        }

        public void Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            TryCreateIfNotExist(type);

            _actions[type].Add(action);
        }
        
        public void Unsubscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (_actions.TryGetValue(type, out var actions))
            {
                actions.Remove(action);
            }
        }
        
        private void TryCreateIfNotExist(Type type)
        {
            if (!_actions.ContainsKey(type))
            {
                _actions.Add(type, new List<object>());
            }
        }
        
#if UNITY_EDITOR
        [ContextMenu("Inject Signal")][Button]
        public void InjectSignal()
        {
            SignalValidator.InjectSignal(this);
        }
#endif
    }
}