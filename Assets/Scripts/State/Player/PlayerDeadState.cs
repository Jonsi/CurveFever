using UnityEngine;

namespace State.Player
{
    public class PlayerDeadState : StateBase
    {
        public override void EnterState()
        {
            Debug.Log("Enter Dead State");//TODO: REMOVE
        }

        public override void ExitState()
        {
            
        }
    }
}