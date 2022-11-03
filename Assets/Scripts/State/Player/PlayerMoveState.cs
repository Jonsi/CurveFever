using System;
using Player;
using UniRx;
using UnityEngine;

namespace State.Player
{
    public class PlayerMoveState : StateBase
    {
        private readonly IPlayer _player;
        private IDisposable _inputSubscription;
    
        public PlayerMoveState(IPlayer player)
        {
            _player = player;
        }
        
        public override void EnterState()
        {
            _inputSubscription =  Observable.EveryFixedUpdate().Subscribe( x => SetDirection(Input.GetAxisRaw("Horizontal")));
        }

        public override void ExitState()
        {
            _inputSubscription.Dispose();
        }
        
        private void SetDirection(float horizontal)
        {
            var direction = Vector2.right * horizontal;
            _player.Move(direction);
        }

    }
}