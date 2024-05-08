using System;
using Source.EasyECS;
using Source.Scripts.Characters;
using Source.Scripts.Ecs.Systems.PerkSystems;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Aura : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _orbitOffset;
        private float _speed;
        private AuraType _type;
        private Action<int> _onTouch;
        private const float Freezing = 10f;
        private const float FreezingDuration = 3f;


        // Инициализация ауры с начальными данными
        public void Initialize(Action<int> onTouch, Transform target, float rotationSpeed, Vector3 offset, AuraType type)
        {
            _onTouch = onTouch;
            _target = target;
            _speed = rotationSpeed;
            _orbitOffset = offset;
            _type = type;

            // Обновите местоположение ауры
            UpdatePosition();
        }

        void Update()
        {
            // Вращение "плоское" вокруг персонажа
            _orbitOffset = Quaternion.Euler(0, 0, _speed * Time.deltaTime) * _orbitOffset;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            transform.position = _target.position + new Vector3(_orbitOffset.x, _orbitOffset.y, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Обрабатываем столкновения для 2D
            if (other.TryGetComponent(out Npc npc))
            {
                switch (_type)
                {
                    case AuraType.Fire:
                        _onTouch.Invoke(npc.Entity);
                        break;
                    case AuraType.Ice:
                        EasyNode.EcsComponenter.Add<FrozenData>(npc.Entity).InitializeValues(Freezing, FreezingDuration);
                        break;
                }
            }
        }
    }

    public enum AuraType
    {
        Fire,
        Ice
    }
}