using Player;
using Tail;
using UnityEngine;

namespace State.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public static class PlayerStateFactory
        {
            public static PlayerMoveState PlayerMoveState(IPlayerController playerController,KeyCode leftButton,KeyCode rightButton)
            {
                return new PlayerMoveState(playerController, leftButton, rightButton);
            }

            public static PlayerDeadState PlayerDeadState(IPlayerController playerController)
            {
                return new PlayerDeadState();
            }
        }
    }
}