using System;
using System.Collections.Generic;
using Player;
using UniRx;
using UnityEngine;

namespace State.Player
{
    public class PlayerMoveState : StateBase
    {
        private readonly IPlayer _player;
        private readonly KeyCode _leftButton;
        private readonly KeyCode _rightButton;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public PlayerMoveState(IPlayer player,KeyCode leftButton, KeyCode rightButton)
        {
            this._player = player;
            _leftButton = leftButton;
            _rightButton = rightButton;
        }
        
        public override void EnterState()
        {
            Observable.EveryFixedUpdate().Subscribe(x => MovePlayer()).AddTo(_disposables);
            Observable.EveryFixedUpdate().Where(x =>Input.GetKey(_leftButton)).Subscribe(x => TurnPlayer(TurnDirection.Left)).AddTo(_disposables);
            Observable.EveryFixedUpdate().Where(x =>Input.GetKey(_rightButton)).Subscribe(x => TurnPlayer(TurnDirection.Right)).AddTo(_disposables);
        }

        public override void ExitState()
        {
            _disposables.ForEach(d => d.Dispose());
        }
        
        private void TurnPlayer(TurnDirection turnDirection) => _player.Turn(turnDirection);
        private void MovePlayer() => _player.MoveForward();
    }
}