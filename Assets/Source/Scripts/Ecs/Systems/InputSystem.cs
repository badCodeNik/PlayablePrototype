using Source.EasyECS;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class InputSystem : EasySystem
    {
        private EcsFilter _playerFilter;
        
        
        protected override void Initialize()
        {
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        protected override void Update()
        {
            var direction = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
            foreach (var playerEntity in _playerFilter)
            {
                if (direction is {x : 0, y : 0}) Componenter.Del<InputData>(playerEntity);
                else
                {
                    ref var inputData = ref Componenter.AddOrGet<InputData>(playerEntity);
                    inputData.InitializeValues(direction);
                    
                }

            }
            
        }
        
       
    }
    
    
    
}