
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Source.SignalSystem
{
    public static class SignalValidator
    {
        private static Signal _signalInstance;

        public static void InjectSignal(Signal signalInstance)
        {
            _signalInstance = signalInstance;
            ValidateAndInjectSignalsInScene();
            ValidateAndInjectSignalsInAssets();
        }

        private static void ValidateAndInjectSignalsInScene()
        {
            var allGameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var gameObject in allGameObjects)
            {
                if (gameObject != null && gameObject.activeInHierarchy)
                {
                    var components = gameObject.GetComponents<Component>();
                    foreach (var component in components)
                    {
                        if (component != null)
                        {
                            InjectSignalIntoFields(component);
                        }
                    }
                }
            }
        }

        private static void ValidateAndInjectSignalsInAssets()
        {
            string[] guids = AssetDatabase.FindAssets("t:GameObject");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    var components = prefab.GetComponents<Component>();
                    foreach (var component in components)
                    {
                        if (component != null)
                        {
                            InjectSignalIntoFields(component);
                        }
                    }
                }
            }
        }

        private static void InjectSignalIntoFields(Component component)
        {
            var fields = component.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Signal))
                {
                    field.SetValue(component, _signalInstance);
                    Debug.Log($"Injected Signal into field {field.Name} of {component.gameObject.name}");
                }
            }
        }
    }
}
#endif
