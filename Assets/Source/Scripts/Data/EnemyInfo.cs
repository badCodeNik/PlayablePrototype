
using Sirenix.OdinInspector;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/NpcInfo")]
    public class EnemyInfo : SerializedScriptableObject
    {
        [SerializeField] private Movable movable;
        [SerializeField] private Destructable destructable;
        [SerializeField] private Attacking attacking;
        
        public Attacking Attacking => attacking;
        public Movable Movable => movable;
        public Destructable Destructable => destructable;
    }
}