using Source.Scripts.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New HeroChannel", menuName = "Events/Hero Channel")]
    public class HeroChannel : ScriptableObject
    {
        public UnityAction<Hero> OnEventRaised;
        public void RaiseEvent(Hero value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}