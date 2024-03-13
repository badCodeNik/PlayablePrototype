using Source.Scripts.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New IntEventChannel", menuName = "Events/Int Event Channel")]
    public class HeroChannel : ScriptableObject
    {
        public UnityAction<Hero> OnEventRaised;
        public void RaiseEvent(Hero value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}