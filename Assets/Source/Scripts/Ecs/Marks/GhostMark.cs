using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Marks
{
    public struct GhostMark : IEcsMark
    {
        public GameObject Ghost;

        public void InitializeValues(GameObject value)
        {
            Ghost = value;
        }
    }
}