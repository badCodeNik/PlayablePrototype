using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.MonoBehaviours
{
    public class AutoTransition : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        private const string Gameplay = "Gameplay";
        public void Transit()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void RestartScene()
        {
            
            SceneManager.LoadScene(Gameplay);
        }
        
    }
}