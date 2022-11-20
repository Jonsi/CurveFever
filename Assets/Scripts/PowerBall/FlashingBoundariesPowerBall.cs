using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class FlashingBoundariesPowerBall : PowerBall
    {
        public override void ApplyPower(IPlayer hitPlayer)
        {
            BoundariesController.Instance.DisableBoundaries();
        }

        public override void UnApplyPower(IPlayer hitTPlayer)
        {
            BoundariesController.Instance.EnableBoundaries();
        }
    }
}