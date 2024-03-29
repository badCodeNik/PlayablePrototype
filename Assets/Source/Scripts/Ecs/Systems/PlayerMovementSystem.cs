using Source.EasyECS;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class PlayerMovementSystem : EasySystem
    {
        private EcsFilter _inputFilter;
        protected override void Initialize()
        {
            _inputFilter = World.Filter<InputData>().Inc<PlayerMark>().Inc<MovableData>().End();
        }

        protected override void Update()
        {
            foreach (var playerEntity in _inputFilter)
            {
                ref var movableData = ref Componenter.Get<MovableData>(playerEntity);
                ref var inputData = ref Componenter.Get<InputData>(playerEntity);
                var speed = movableData.MoveSpeed * Time.fixedDeltaTime;
                movableData.CharacterTransform.Translate(inputData.Direction.normalized * speed);
                RegistryEvent(new OnMoveEvent(){Direction = inputData.Direction,Entity = playerEntity});
            }
        }
    }
}