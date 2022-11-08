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
            this._player = player;
        }
        
        public override void EnterState()
        {
            _inputSubscription =  Observable.EveryFixedUpdate().Subscribe( x => RotatePlayer(Input.GetAxisRaw("Horizontal")));
        }

        public override void ExitState()
        {
            _inputSubscription.Dispose();
        }
        
        private void RotatePlayer(float inputAxis)
        {
            var direction = Vector2.right * inputAxis;
            _player.Rotate(direction);
        }

    }
}