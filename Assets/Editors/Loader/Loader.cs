using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editors.Loader
{
    public class Loader : EditorWindow
    {
        private const string BootstrapperPath = "Assets/Scenes/Bootstrap.unity";
        private const string GameplayPath = "Assets/Scenes/Gameplay.unity";
        private const string MenuPath = "Assets/Scenes/Menu.unity";
        [MenuItem("Window/Loader")]
        public static void ShowWindow()
        {
            GetWindow<Loader>("Loader");
        }
        
        [MenuItem("Tools/Start Game")]
        public static void StartGame()
        {
            EditorSceneManager.OpenScene(BootstrapperPath);
            EditorApplication.isPlaying = true;
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Start Game"))
            {
                EditorSceneManager.OpenScene(BootstrapperPath);
                EditorApplication.isPlaying = true;
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Bootstrap"))
            {
                EditorSceneManager.OpenScene(BootstrapperPath);
            }
            if (GUILayout.Button("Menu"))
            {
                EditorSceneManager.OpenScene(MenuPath);
            }
            if (GUILayout.Button("Gameplay"))
            {
                EditorSceneManager.OpenScene(GameplayPath);
            }
            GUILayout.EndHorizontal();
        }
    }
}
