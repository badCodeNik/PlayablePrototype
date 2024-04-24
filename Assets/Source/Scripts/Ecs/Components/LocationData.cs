using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct LocationData : IEcsData<Transform>
    {
        public Transform FinishingPoint;
        
        public void InitializeValues(Transform value)
        {
            FinishingPoint = value;
        }
    }
}