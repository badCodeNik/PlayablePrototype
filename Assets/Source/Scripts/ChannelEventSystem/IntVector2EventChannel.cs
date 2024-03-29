using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New IntVector2EventChannel", menuName = "Events/Int Vector2 Event Channel")]
    public class IntVector2EventChannel : ScriptableObject
    {
        public UnityAction<int, Vector2> OnEventRaised;
        public void RaiseEvent(int value, Vector2 vector)
        {
            OnEventRaised?.Invoke(value, vector);
        }
    }
}