using Player;
using UnityEngine;

namespace PowerUps
{
    public class SpeedPowerUp : PowerUpBase
    {
        [SerializeField] private float _speed = 1;
        private float _initialSpeed;
        protected override void ApplyPowerUp(IPlayer player)
        {
            _initialSpeed = player.GetSpeed();
            player.SetSpeed(_speed);
        }

        protected override void UnApplyPowerUp(IPlayer player)
        {
            player.SetSpeed(_initialSpeed);
        }
    }
}