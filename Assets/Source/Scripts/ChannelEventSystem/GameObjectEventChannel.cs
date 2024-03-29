using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New GameObjectEventChannel", menuName = "Events/GameObject Event Channel")]
    public class GameObjectEventChannel : ScriptableObject
    {
        public UnityAction<GameObject> OnEventRaised;
        public void RaiseEvent(GameObject value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}