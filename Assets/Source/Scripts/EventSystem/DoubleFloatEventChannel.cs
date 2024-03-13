using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New DoubleFloatEventChannel", menuName = "Events/Double Float Event Channel")]
    public class DoubleFloatEventChannel : ScriptableObject
    {
        public UnityAction<float, float> OnEventRaised;
        public void RaiseEvent(float first, float second)
        {
            OnEventRaised?.Invoke(first, second);
        }
    }
}