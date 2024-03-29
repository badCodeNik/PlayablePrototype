using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New Collider2DEventChannel", menuName = "Events/Collider2DEventChannel Event Channel")]
    public class Collider2DEventChannel : ScriptableObject
    {
        public UnityAction<Collider2D> OnEventRaised;
        public void RaiseEvent( Collider2D collider)
        {
            OnEventRaised?.Invoke( collider);
        }
    }
}