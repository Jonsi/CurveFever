using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class SpeedPowerBall : PlayerPowerBall
    {
        [Header("Speed Settings")]
        [SerializeField] private float _speedMultiplier = 2;

        protected override void ApplyOnPlayer(IPlayer player)
        {
            player.SetSpeed(player.GetSpeed() * _speedMultiplier);
            player.SetRotationSpeed(player.GetRotationSpeed() * _speedMultiplier );
        }

        protected override void UnApplyOnPlayer(IPlayer player)
        {
            player.SetSpeed(player.GetSpeed() / _speedMultiplier);
            player.SetRotationSpeed(player.GetRotationSpeed() /_speedMultiplier );
        }
    }
}