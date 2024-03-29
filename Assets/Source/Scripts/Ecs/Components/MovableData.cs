using Source.EasyECS.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Ecs.Components
{
    public struct MovableData : IEcsComponent
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public Transform CharacterTransform;
        public NavMeshAgent NavMeshAgent;

        public void InitializeValue(float moveSpeed, float rotationSpeed, Transform characterTransform, NavMeshAgent navMeshAgent )
        {
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
            CharacterTransform = characterTransform;
            NavMeshAgent = navMeshAgent;
        }
    }
}