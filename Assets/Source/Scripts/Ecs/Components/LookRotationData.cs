using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct LookRotationData : IEcsData<Vector2>
    {
        public Vector2 Direction;
        public void InitializeValues(Vector2 value)
        {
            Direction = value;
        }
    }
}