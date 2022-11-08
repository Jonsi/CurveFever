using Player;
using UnityEngine;

namespace PowerUps
{
    public class SpeedPowerUp : PowerUpBase
    {
        [SerializeField] private float _speed = 1;
        private float _initialSpeed;
        protected override void ApplyPowerUp(IPlayerController playerController)
        {
            _initialSpeed = playerController.GetSpeed();
            playerController.SetSpeed(_speed);
        }

        protected override void UnApplyPowerUp(IPlayerController playerController)
        {
            playerController.SetSpeed(_initialSpeed);
        }
    }
}