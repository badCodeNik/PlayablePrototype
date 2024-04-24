using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;

namespace Source.Scripts.Ecs.Systems
{
    public class AnimatorListener : EcsEventListener<OnMoveEvent, OnEnemyMoveEvent>
    {
        public override void OnEvent(OnMoveEvent data)
        {
            Componenter.TryGetReadOnly(data.Entity, out AnimatorData animatorData);
            animatorData.Value.SetFloat("xMove", data.Direction.x);
            animatorData.Value.SetFloat("yMove", data.Direction.y);
            if (Componenter.Has<PerkChoosingMark>(data.Entity))
            {
                animatorData.Value.SetFloat("xMove",0);
                animatorData.Value.SetFloat("yMove",0);
            }
        }

        public override void OnEvent(OnEnemyMoveEvent data)
        {
            Componenter.TryGetReadOnly(data.Entity, out AnimatorData animatorData);
            animatorData.Value.SetFloat("xMove", data.Direction.x);
            animatorData.Value.SetFloat("yMove", data.Direction.y);
        }

        
    }
}