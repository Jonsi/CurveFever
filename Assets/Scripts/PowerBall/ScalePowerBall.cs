using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class ScalePowerBall : PlayerPowerBall
    {
        [SerializeField] private float _scaleMultiplier;
        protected override void ApplyOnPlayer(IPlayer player)
        {
            player.Scale(_scaleMultiplier);
        }

        protected override void UnApplyOnPlayer(IPlayer player)
        {
            player.Scale(1/_scaleMultiplier);
        }
    }
}
