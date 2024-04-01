#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Source.SignalSystem
{
    public static class SignalValidator
    {
        public static void InjectSignal(Signal signalInstance)
        {
            ValidateAndInjectSignalsInScenes(signalInstance);
            ValidateAndInjectSignalsInAssets(signalInstance);
        }

        private static void ValidateAndInjectSignalsInScenes(Signal signalInstance)
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
                                    InjectSignalIntoFields(component, signalInstance);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void ValidateAndInjectSignalsInAssets(Signal signalInstance)
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
                            InjectSignalIntoFields(component, signalInstance);
                        }
                    }
                }
            }
        }

        private static void InjectSignalIntoFields(Component component, Signal signalInstance)
        {
            var fields = component.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Signal))
                {
                    field.SetValue(component, signalInstance);
                    Debug.Log($"Injected Signal into field {field.Name} of {component.gameObject.name}");
                }
            }
        }
    }
}
#endif