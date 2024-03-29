using Source.SignalSystem;
using UnityEngine;
using UnityEngine.UI;


namespace Source.Scripts.UI
{
    public class HealthBar : MonoSignalListener<OnHealthChange>
    {
        [SerializeField] private Slider slider;
        
        protected override void OnSignal(OnHealthChange data)
        {
            slider.maxValue = data.MaxHealth;
            slider.value = data.CurrentHealth;
        }
    }

    public struct OnHealthChange
    {
        public float MaxHealth;
        public float CurrentHealth;
    }
}