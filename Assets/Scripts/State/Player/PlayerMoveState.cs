using System;
using System.Collections.Generic;
using Player;
using UniRx;
using UnityEngine;

namespace State.Player
{
    public class PlayerMoveState : StateBase
    {
        private readonly IPlayerController _playerController;
        private readonly KeyCode _leftButton;
        private readonly KeyCode _rightButton;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public PlayerMoveState(IPlayerController playerController,KeyCode leftButton, KeyCode rightButton)
        {
            this._playerController = playerController;
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
        
        private void TurnPlayer(TurnDirection turnDirection) => _playerController.Turn(turnDirection);
        private void MovePlayer() => _playerController.MoveForward();
    }
}