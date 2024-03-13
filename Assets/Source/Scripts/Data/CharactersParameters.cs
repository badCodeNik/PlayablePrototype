using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.Data
{
    
    [Serializable][Toggle("enabled")]
    public abstract class Parameter
    {
        [SerializeField] protected bool enabled;

        public bool Enabled => enabled;
    }
    
    [Serializable]
    public class Movable : Parameter
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float rotationSpeed;

        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
    }
    
    [Serializable]
    public class Interactable : Parameter
    {
        [SerializeField] private float interactableSpeed;

        public float InteractableSpeed => interactableSpeed;
    }
    
    [Serializable]
    public class Destructable : Parameter
    {
        [SerializeField] private float health;

        public float Health => health;
    }
}