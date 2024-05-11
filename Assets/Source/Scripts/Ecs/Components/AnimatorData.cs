using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct AnimatorData : IEcsComponent
    {
        public Animator Value;
        public void InitializeValues(Animator value, bool isMoving = false)
        {
            Value = value;
        }
    }
}