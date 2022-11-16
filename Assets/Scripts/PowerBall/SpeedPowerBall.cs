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
            base.ApplyOnPlayer(player);
            player.SetSpeed(player.GetSpeed() * _speedMultiplier);
            player.SetRotationSpeed(player.GetRotationSpeed() * _speedMultiplier );
        }

        protected override void UnApplyOnPlayer(IPlayer player)
        {
            var stateData = playerToStateData[player]; 
            player.SetSpeed(stateData.Speed);
            player.SetRotationSpeed(stateData.RotationSpeed);
        }
    }
}