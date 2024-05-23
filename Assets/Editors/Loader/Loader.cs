using UnityEditor;
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
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(BootstrapperPath);
            EditorApplication.isPlaying = true;
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Start Game"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(BootstrapperPath);
                EditorApplication.isPlaying = true;
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Bootstrap"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(BootstrapperPath);
            }
            if (GUILayout.Button("Menu"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(MenuPath);
            }
            if (GUILayout.Button("Gameplay"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(GameplayPath);
            }
            GUILayout.EndHorizontal();
        }
    }
}
