using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.Modules
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent OnStartInteract;
        public UnityEvent OnEndInteract;
        public UnityEvent OnInterruptInteract;
    }
}