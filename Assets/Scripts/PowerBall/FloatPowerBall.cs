using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class FloatPowerBall : PlayerPowerBall
    {
        protected override void ApplyOnPlayer(IPlayer player)
        {
            player.SetKillable(false);
        }

        protected override void UnApplyOnPlayer(IPlayer player)
        {
            player.SetKillable(true);
        }
    }
}