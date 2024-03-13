
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/NpcInfo")]
    public class NpcInfo : SerializedScriptableObject
    {
        [SerializeField] private Movable movable;
        [SerializeField] private Interactable interactable;
        [SerializeField] private Destructable destructable;

        public Movable Movable => movable;
        public Interactable Interactable => interactable;
        public Destructable Destructable => destructable;
    }
}