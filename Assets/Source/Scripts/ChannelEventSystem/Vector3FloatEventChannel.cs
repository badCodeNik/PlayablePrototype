using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    
    
        [CreateAssetMenu(fileName = "New Vector3FloatEventChannel", menuName = "Events/Vector3 Float Event Channel")]
        public class Vector3FloatEventChannel : ScriptableObject
        {
            public UnityAction<Vector3,float> OnEventRaised;
            public void RaiseEvent(Vector3 direction,float value)
            {
                OnEventRaised?.Invoke(direction,value);
            }
        }
    
}