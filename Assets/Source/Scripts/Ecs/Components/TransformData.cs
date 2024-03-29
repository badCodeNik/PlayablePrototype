using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct TransformData : IEcsData<Transform>
    {
        public Transform Value;
        public void InitializeValues(Transform value)
        {
            Value = value;
        }
    }
}