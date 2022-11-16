using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PowerBall
{
    
    [RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
    public class PowerBallBehaviour : MonoBehaviour
    {
        [SerializeField] protected float duration = 5f;

        private SpriteRenderer _spriteRenderer;
        private IPlayer _hitPlayer;

        private IDisposable _collisionSubscription;

        public PowerBall PowerBall;
        
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetColor();
            _spriteRenderer.sprite = PowerBall.Sprite;
        }

        private void OnEnable()
        {
            _collisionSubscription = this.OnTriggerEnter2DAsObservable().Subscribe(OnHit).AddTo(this);
        }

        private async void OnHit(Collider2D other)
        {
            if (other.TryGetComponent<PlayerCollider>(out var playerCollider) == false)
            {
                return;
            }

            _hitPlayer = playerCollider.Player;
            _collisionSubscription.Dispose();
            _spriteRenderer.gameObject.SetActive(false);
            PowerBall.ApplyPower(_hitPlayer);
            await UniTask.Delay((int) duration * 1000);
            if (_hitPlayer != null)
            {
                PowerBall.UnApplyPower(_hitPlayer);
            }
            Destroy(gameObject);//TODO: ADD BACK TO POOL
        }
        
                
        private void SetColor()
        {
            _spriteRenderer.color = PowerBall.GetColor();
        }
    }
}
