using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.Ecs.Components;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class BlinkSystem : EasySystem
    {
        private EcsFilter _blinkingFilter;

        protected override void Initialize()
        {
            _blinkingFilter = World.Filter<BlinkingData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _blinkingFilter)
            {
                ref var blinkingData = ref Componenter.Get<BlinkingData>(entity);
                ref var spriteData = ref Componenter.Get<SpriteData>(entity);
                blinkingData.Timer -= DeltaTime;
                blinkingData.TimeRemaining -= DeltaTime;
                var interval = blinkingData.BlinkingInterval;

                if (blinkingData.Timer < 0)
                {
                    spriteData.SpriteRenderer.color = Color.red;
                    blinkingData.Timer += interval;
                }
                else
                {
                    spriteData.SpriteRenderer.color = Color.white;
                }

                if (blinkingData.TimeRemaining < 0)
                {
                    Componenter.Del<BlinkingData>(entity);
                }
            }
        }
    }

    public struct BlinkingData : IEcsComponent
    {
        public float BlinkingInterval;
        public float Timer;
        public float TimeRemaining;

        public void InitializeValues(float blinkingInterval, float timeRemaining)
        {
            BlinkingInterval = blinkingInterval;
            TimeRemaining = timeRemaining;
        }
    }
}