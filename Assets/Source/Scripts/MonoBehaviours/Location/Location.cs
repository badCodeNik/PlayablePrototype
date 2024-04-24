using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Ecs.Components;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours.Location
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private Signal signal;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private Transform finishingPoint;
        [SerializeField, ReadOnly] private int entity;

        public Transform PlayerSpawnPosition => playerSpawnPosition;

        private void Start()
        {
            signal.RegistryRaise(new OnLocationCreatedSignal()
            {
                PlayerSpawnPosition = playerSpawnPosition
            });

            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            ref var locationData = ref componenter.AddOrGet<LocationData>(entity);
            locationData.InitializeValues(finishingPoint);
            
            

        }
        
    }
    
}