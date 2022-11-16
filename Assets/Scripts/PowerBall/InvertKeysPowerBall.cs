using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class InvertKeysPowerBall : PlayerPowerBall
    {
        protected override void ApplyOnPlayer(IPlayer player)
        {
            player.InvertDirectionInput();
        }

        protected override void UnApplyOnPlayer(IPlayer player)
        {
            player.InvertDirectionInput();
        }
    }
}