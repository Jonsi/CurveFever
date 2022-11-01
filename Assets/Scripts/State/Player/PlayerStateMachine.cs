using Player;

namespace State.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public static class PlayerStateFactory
        {
            public static PlayerMoveState PlayerMoveState(IPlayer player)
            {
                return new PlayerMoveState(player);
            }

            public static PlayerDeadState PlayerDeadState(IPlayer player)
            {
                return new PlayerDeadState();
            }
        }
    }
}