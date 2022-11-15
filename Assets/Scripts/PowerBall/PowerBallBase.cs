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
    public abstract class PowerBallBase : MonoBehaviour
    {
        [SerializeField] protected float duration = 5f;
        
        protected SpriteRenderer spriteRenderer;
        protected IPlayer hitPlayer;
        
        private IDisposable _collisionSubscription;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
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

            hitPlayer = playerCollider.Player;
            _collisionSubscription.Dispose();
            spriteRenderer.gameObject.SetActive(false);
            ApplyPower();
            await UniTask.Delay((int) duration * 1000);
            if (hitPlayer != null)
            {
                UnApplyPower();
            }
            Destroy(gameObject);//TODO: ADD BACK TO POOL
        }

        protected abstract void ApplyPower();
        protected abstract void UnApplyPower();

    }
}
