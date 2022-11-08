using System;
using Cysharp.Threading.Tasks;
using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PowerUps
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
    public abstract class PowerUpBase : MonoBehaviour
    {
        [SerializeField] protected float duration = 5f;
        private IDisposable _collisionSubscription;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _collisionSubscription =  this.OnTriggerEnter2DAsObservable().Subscribe(OnHit);
        }

        private async void OnHit(Collider2D other)
        {
            if (other.TryGetComponent<IPlayerController>(out var player) == false)
            {
                return;
            }
            
            _collisionSubscription.Dispose();
            _spriteRenderer.gameObject.SetActive(false);
            ApplyPowerUp(player);
            print("Started PowerUp!");//TODO: REMOVE
            await UniTask.Delay((int) duration * 1000);
            if (player != null)
            {
                UnApplyPowerUp(player);
                print("UnApplied PowerUp!");//TODO: REMOVE

            }
            Destroy(gameObject);//TODO: ADD BACK TO POOL
        }

        protected abstract void ApplyPowerUp(IPlayerController playerController);
        protected abstract void UnApplyPowerUp(IPlayerController playerController);
    }
}
