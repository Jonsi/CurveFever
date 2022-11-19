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

        private PowerBall _powerBall;
        
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _collisionSubscription = this.OnTriggerEnter2DAsObservable().Subscribe(OnHit).AddTo(this);
        }

        public void InitializeUsingSettings(PowerBall powerBall)
        {
            _powerBall = powerBall;
            _spriteRenderer.sprite = _powerBall.Sprite;
            SetColor();
        }

        protected virtual async void OnHit(Collider2D other)
        {
            if (other.TryGetComponent<PlayerCollider>(out var playerCollider) == false)
            {
                return;
            }

            _hitPlayer = playerCollider.Player;
            _collisionSubscription.Dispose();
            _spriteRenderer.gameObject.SetActive(false);
            _powerBall.ApplyPower(_hitPlayer);
            await UniTask.Delay((int) duration * 1000);
            if (_hitPlayer != null)
            {
                _powerBall.UnApplyPower(_hitPlayer);
            }
            Destroy(gameObject);//TODO: ADD BACK TO POOL
        }
        
                
        private void SetColor()
        {
            _spriteRenderer.color = _powerBall.GetColor();
        }
    }
}
