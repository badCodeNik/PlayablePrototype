using System;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Characters
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private bool isInitialized;
        [SerializeField] private CharacterFaction faction;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Action<int> _onTouch;
        private int _touchCount;
        private int _destroyTouchAmount = 3;


        private void OnValidate()
        {
            if (_rb == null) _rb.GetComponent<Rigidbody2D>();
            if (_collider == null) _collider.GetComponent<Collider2D>();
        }

        public void Initialize(Action<int> onTouch, Vector2 velocity, CharacterFaction faction, Sprite projectileSprite)
        {
            _onTouch = onTouch;
            _rb.velocity = velocity;
            this.faction = faction;
            spriteRenderer.sprite = projectileSprite;
            switch (this.faction)
            {
                case CharacterFaction.Player:
                    _collider.includeLayers += LayerMask.GetMask("Enemy");
                    break;
                case CharacterFaction.Enemy:
                    _collider.includeLayers += LayerMask.GetMask("Player");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            isInitialized = true;
            
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (!isInitialized) return;
            
            _touchCount++;
            switch (faction)
            {
                case CharacterFaction.Player:
                    if (other.TryGetComponent(out Npc npc))
                    {
                        _onTouch.Invoke(npc.Entity);
                        if (_touchCount >= _destroyTouchAmount) DestroyProjectile();
                    }
                    break;
                case CharacterFaction.Enemy:
                    if (other.TryGetComponent(out Hero player))
                    {
                        _onTouch.Invoke(player.Entity);
                        if (_touchCount >= _destroyTouchAmount) DestroyProjectile();
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void DestroyProjectile()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}