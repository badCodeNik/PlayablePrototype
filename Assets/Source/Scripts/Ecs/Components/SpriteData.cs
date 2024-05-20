using Source.EasyECS.Interfaces;
using UnityEngine;

namespace Source.Scripts.Ecs.Components
{
    public struct SpriteData : IEcsComponent
    {
        public SpriteRenderer SpriteRenderer;
    }
}