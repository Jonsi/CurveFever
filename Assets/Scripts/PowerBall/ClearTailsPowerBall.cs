using Events;
using Managers;
using Player;
using UnityEngine;

namespace PowerBall
{
    [CreateAssetMenu]
    public class ClearTailsPowerBall : PowerBall
    {
        [SerializeField] private VoidGameEvent _tailClearingEvent;
        public override void ApplyPower(IPlayer hitPlayer)
        {
            _tailClearingEvent.Invoke();
            TailManager.Instance.RemoveAllTails();
        }

        public override void UnApplyPower(IPlayer hitTPlayer)
        {
        
        }
    }
}