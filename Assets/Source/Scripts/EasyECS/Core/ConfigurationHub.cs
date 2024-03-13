using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.EasyECS
{
    public class ConfigurationHub : EasyMonoBehaviour
    {
        [SerializeField] private List<Configuration> configurations;
        private Dictionary<Type, Configuration> _data;

        public void PreInitialize()
        {
            _data = new Dictionary<Type, Configuration>();
            foreach (var elementUI in configurations)
            {
                _data[elementUI.GetType()] = elementUI;
            }
        }

        public T GetConfigByType<T>() where T : Configuration
        {
            return (T)_data[typeof(T)];
        }
        
#if UNITY_EDITOR
        [Button]
        public void GetAllData()
        {
            configurations = new List<Configuration>();

            Configuration[] allConfigurations = Resources.FindObjectsOfTypeAll<Configuration>();

            foreach (var configurationInstance in allConfigurations)
            {
                configurations.Add(configurationInstance);
            }
        }
#endif
    }
        
    public abstract class Configuration: SerializedScriptableObject
    {

    }
}