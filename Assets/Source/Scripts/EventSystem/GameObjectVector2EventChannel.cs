using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New GameObjectVector2EventChannel", menuName = "Events/GameObject Vector2 Event Channel")]
    public class GameObjectVector2EventChannel : ScriptableObject
    {
        public UnityAction<GameObject, Vector2> OnEventRaised;
        public void RaiseEvent(GameObject value, Vector2 vector2)
        {
            OnEventRaised?.Invoke(value, vector2);
        }
    }
}