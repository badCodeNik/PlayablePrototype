using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    
    [CreateAssetMenu(fileName = "New FloatEventChannel", menuName = "Events/Float Event Channel")]
    public class FloatEventChannel : ScriptableObject
    {
        public UnityAction<float> OnEventRaised;
        public void RaiseEvent(float value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}