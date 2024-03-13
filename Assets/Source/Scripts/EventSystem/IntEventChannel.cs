using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New IntEventChannel", menuName = "Events/Int Event Channel")]
    public class IntEventChannel : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;
        public void RaiseEvent(int value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}