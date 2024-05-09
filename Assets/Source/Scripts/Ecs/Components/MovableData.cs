using Source.EasyECS.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Ecs.Components
{
    public struct MovableData : IEcsComponent
    {
        public float MoveSpeed;
        public Transform CharacterTransform;
        public NavMeshAgent NavMeshAgent;

        public void InitializeValue(float moveSpeed,  Transform characterTransform, NavMeshAgent navMeshAgent )
        {
            MoveSpeed = moveSpeed;
            CharacterTransform = characterTransform;
            NavMeshAgent = navMeshAgent;
        }
    }
}