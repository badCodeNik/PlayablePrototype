using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.MonoBehaviours;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class HealthChanger : EcsEventListener<OnProjectileTouch>
    {
        public override void OnEvent(OnProjectileTouch data)
        {
            ref var targetHealth = ref Componenter.Get<DestructableData>(data.TargetEntity).CurrentHealth;
            ref var playerAttackingData = ref Componenter.Get<AttackingData>(data.CharacterEntity);
            targetHealth -= playerAttackingData.Damage;
            if (targetHealth <= 0)
            {
                Componenter.Add<DestroyingData>(data.TargetEntity).InitializeValues(6);
                
            }
        }
    }
}