using Player;
using Tail;
using UnityEngine;

namespace State.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public static class PlayerStateFactory
        {
            public static PlayerMoveState PlayerMoveState(IPlayer player,KeyCode leftButton,KeyCode rightButton)
            {
                return new PlayerMoveState(player, leftButton, rightButton);
            }

            public static PlayerDeadState PlayerDeadState(IPlayer player)
            {
                return new PlayerDeadState();
            }
        }
    }
}