using Source.Scripts.KeysHolder;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    
    [CreateAssetMenu(fileName = "New LanguageKeyEventChannel", menuName = "Events/Language Key Event Channel")]
    public class LanguageKeyEventChannel : ScriptableObject
    {
        public UnityEvent<LanguageKeys> OnEventRaised;
        public void RaiseEvent(LanguageKeys value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}