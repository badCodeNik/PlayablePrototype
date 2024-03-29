using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New VoidEventChannel", menuName = "Events/Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}