using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;

namespace Source.Scripts.Ecs.ECSeventListeners
{
    public class HealthChanger : EcsEventListener<OnHitEvent>
    {
        public override void OnEvent(OnHitEvent data)
        {
            ref var targetHealth = ref Componenter.Get<DestructableData>(data.TargetEntity).CurrentHealth;
            ref var playerAttackingData = ref Componenter.Get<AttackingData>(data.CharacterEntity);
            targetHealth -= playerAttackingData.Damage;
            if (targetHealth <= 0)
            {
                Componenter.Add<DestroyingData>(data.TargetEntity).InitializeValues(3);
                
            }
        }
    }
}