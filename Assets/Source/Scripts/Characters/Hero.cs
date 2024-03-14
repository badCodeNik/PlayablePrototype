
using System;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Data;
using Source.Scripts.Ecs.Components;
using Source.Scripts.EventSystem;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.Characters
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private HeroChannel heroChannel;
        [SerializeField, ReadOnly] private int entity;

        public HeroInfo HeroInfo => heroInfo;
        public int Entity => entity;

        public void Start()
        {
            EcsInitialize();
            heroChannel.RaiseEvent(this);
        }

        public void EcsInitialize()
        {
            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            
            // Создаем и прокидываем соотвествующую дату в ECS!
            if (heroInfo.Movable.Enabled)
            {
                ref var movableData = ref componenter.Add<MovableData>(entity);
                movableData.RotationSpeed = heroInfo.Movable.RotationSpeed;
                movableData.MoveSpeed = heroInfo.Movable.MoveSpeed;
            }
            
            // И все остальные параметры...

            if (heroInfo.Interactable.Enabled)
            {
                
            }

            if (heroInfo.Destructable.Enabled)
            {
                
            }

        }
    }
}