using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct AnimatorData : IEcsData<Animator>
    {
        public Animator Value;
        public void InitializeValues(Animator value)
        {
            Value = value;
        }
    }
}