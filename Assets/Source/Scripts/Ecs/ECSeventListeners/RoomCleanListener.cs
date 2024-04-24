using Source.EasyECS;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Ecs.Systems
{
    public class RoomCleanListener : EcsEventListener<OnRoomCleaned>
    {
        private EcsFilter _locationFilter;
        private EcsFilter _playerFilter;

        protected override void Initialize()
        {
            _locationFilter = World.Filter<LocationData>().End();
            _playerFilter = World.Filter<PlayerMark>().End();
        }

        public override void OnEvent(OnRoomCleaned data)
        {
            var isRoomClean = true;
            if (_playerFilter.TryGetFirstEntity(out int playerEntity))
            {
                foreach (var locationEntity in _locationFilter)
                {
                    ref var locationData = ref Componenter.Get<LocationData>(locationEntity);
                    if (Vector3.Distance(locationData.FinishingPoint.position, data.Transform.position) < 20)
                    {
                        if (isRoomClean)
                        {
                            RegistrySignal(new OnRoomCleanedSignal());
                            Componenter.Add<PerkChoosingMark>(playerEntity);
                            isRoomClean = false;
                        }
                    }
                }
            }
        }
    }
}