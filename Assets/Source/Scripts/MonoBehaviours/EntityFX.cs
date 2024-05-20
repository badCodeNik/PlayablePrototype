using Source.Scripts.EasyECS.Core;
using Source.Scripts.EasyECS.Custom;
using Source.Scripts.Ecs.Components;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class EntityFX : EcsEventListener<OnHitEvent>
    {
        private float _blinkInterval; // Интервал между миганиями
        private float _lastBlinkTime; // Время последнего мигания
        private int _blinkCount; // Количество совершенных миганий
        private readonly float _flashDuration = 1;
        private bool _isBlinking;
        private SpriteRenderer _spriteRenderer;

        protected override void Update()
        {
            if (_isBlinking && Time.time - _lastBlinkTime >= _blinkInterval)
            {
                RedColorBlink();
                _blinkCount++;

                if (_blinkCount >= 6) // Поскольку переключение с красного на белый считается как 2 мигания
                {
                    CancelRedBlink();
                    _isBlinking = false;
                }

                _lastBlinkTime = Time.time;
            }
        }


        private void RedColorBlink()
        {
            _spriteRenderer.color = _spriteRenderer.color == Color.white ? Color.red : Color.white;
        }

        private void CancelRedBlink()
        {
            _spriteRenderer.color = Color.white;
            _isBlinking = false; // Завершаем процесс моргания
        }

        public override void OnEvent(OnHitEvent data)
        {
            _spriteRenderer = Componenter.Get<SpriteData>(data.TargetEntity).SpriteRenderer;
            if (_spriteRenderer != null)
            {
                _lastBlinkTime = Time.time;
                _blinkCount = 0;
                _isBlinking = true;
                _blinkInterval = _flashDuration / 6; // Будем мигать 3 раза: 3 моргания * 2 переключения цвета
            }
        }
    }
}