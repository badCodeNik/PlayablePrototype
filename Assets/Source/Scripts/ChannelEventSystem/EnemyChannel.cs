using Source.Scripts.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    
    [CreateAssetMenu(fileName = "New EnemyChannel", menuName = "Events/Enemy Channel")]
    public class EnemyChannel : ScriptableObject
    {
        public UnityAction<Npc> OnEventRaised;
        public void RaiseEvent(Npc value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
    
}