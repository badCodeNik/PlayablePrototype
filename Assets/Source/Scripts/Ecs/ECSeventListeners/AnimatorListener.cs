using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;

namespace Source.Scripts.Ecs.Systems
{
    public class AnimatorListener : EcsEventListener<OnMoveEvent,OnEnemyMoveEvent>
    {
        public override void OnEvent(OnMoveEvent data)
        {
            Componenter.TryGetReadOnly(data.Entity, out AnimatorData animatorData);
            animatorData.Value.SetFloat("xMove", data.Direction.x);
            animatorData.Value.SetFloat("yMove", data.Direction.y);
        }

        public override void OnEvent(OnEnemyMoveEvent data)
        {
            Componenter.TryGetReadOnly(data.Entity, out AnimatorData animatorData);
            animatorData.Value.SetFloat("xMove", data.Direction.x);
            animatorData.Value.SetFloat("yMove", data.Direction.y);
        }
    }
}