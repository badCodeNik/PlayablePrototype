using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New VectorEventChannel", menuName = "Events/Vector Event Channel")]
    public class Vector2EventChannel : ScriptableObject
    {
        public UnityAction<Vector2> OnEventRaised;
        public void RaiseEvent(Vector2 value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}