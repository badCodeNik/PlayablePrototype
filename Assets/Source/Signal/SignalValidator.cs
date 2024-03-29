#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Source.SignalSystem
{
    public static class SignalValidator
    {
        private static Signal _signalInstance;

        public static void InjectSignal(Signal signalInstance)
        {
            _signalInstance = signalInstance;
            ValidateAndInjectSignalsInScenes();
            ValidateAndInjectSignalsInAssets();
        }

        private static void ValidateAndInjectSignalsInScenes()
        {
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                var scene = EditorSceneManager.GetSceneAt(i);
                if (scene.isLoaded)
                {
                    var rootGameObjects = scene.GetRootGameObjects();
                    foreach (var rootGameObject in rootGameObjects)
                    {
                        if (rootGameObject != null && rootGameObject.activeInHierarchy)
                        {
                            var components = rootGameObject.GetComponents<Component>();
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