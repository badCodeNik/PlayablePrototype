using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Source.Scripts.UI
{
    public class AutoTransition : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        
        public void Transit()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}