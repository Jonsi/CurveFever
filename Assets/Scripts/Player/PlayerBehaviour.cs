using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Events;
using State.Player;
using Tail;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour,IPlayer,IHittable
    {
        [Header("Components")]
        [SerializeField] private HeadMovementController _headMovementController;
        [SerializeField] private AutoTailDrawer _tailDrawer;

        [Header("Events")]
        [SerializeField] private VoidGameEvent _gameStartedEvent;

        [SerializeField] private VoidGameEvent _tailsClearingEvent;
        
        private KeyCode _leftButton;
        private KeyCode _rightButton;
        private float _moveSpeed = 1;
        private float _rotationSpeed = 100;
        private bool _killable = true;
        
        private readonly PlayerStateMachine _stateMachine = new PlayerStateMachine();
        
        private void OnGameStarted()
        {
            _headMovementController.transform.RotateTowards(Vector2.zero);
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerMoveState(this,_leftButton,_rightButton));
            _tailDrawer.StartDraw();
        }

        public void InitializeUsingSettings(PlayerSettings playerSettings)
        {
            _leftButton = playerSettings.LeftKey;
            _rightButton = playerSettings.RightKey;
            _moveSpeed = playerSettings.StartSpeed;
            _rotationSpeed = playerSettings.StartRotationSpeed;
            _headMovementController.GetComponent<SpriteRenderer>().color = playerSettings.HeadColor;
        }

        private void OnEnable()
        {
            _gameStartedEvent.RegisterListener(OnGameStarted);
            _tailsClearingEvent.RegisterListener(OnTailClearing);
        }

        private async void OnTailClearing()
        {
            await _tailDrawer.StopDraw();
            _tailDrawer.StartDraw();
        }

        private void OnDisable()
        {
            _gameStartedEvent.UnRegisterListener(OnGameStarted);
            _tailsClearingEvent.RegisterListener(OnTailClearing);
        }

        public float GetSpeed() => _moveSpeed;
        public void Scale(float multiplier)
        {
            _headMovementController.transform.localScale *= multiplier;
            _tailDrawer.Scale(multiplier);
        }

        public float GetRotationSpeed() => _rotationSpeed;
        public void SetSpeed(float speed) => _moveSpeed = speed;
        public void SetRotationSpeed(float speed) => _rotationSpeed = speed;
        public void Turn(TurnDirection direction) => _headMovementController.Turn(direction,_rotationSpeed);
        public void MoveForward() => _headMovementController.MoveForward(_moveSpeed);
        public void InvertDirectionInput()
        {
            var rightKey = _rightButton;
            var leftKey = _leftButton;

            _rightButton = leftKey;
            _leftButton = rightKey;
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerMoveState(this,_leftButton,_rightButton));
        }

        public void SetKillable(bool killable)
        {
            if (killable == true)
            {
                _tailDrawer.StartDraw();
                _headMovementController.StopFlicker();
            }
            else
            {
                _headMovementController.StartFlicker();
                _tailDrawer.StopDraw().Forget();
            }

            _killable = killable;
        }

        public void Teleport(Vector2 position)
        {
            if (WorldArea.IsInsidePerimeters(position) == false)
            {
                return;
            }

            if (_tailDrawer.IsDrawing)
            {
                _tailDrawer.StopDraw().Forget();
                _headMovementController.transform.position = position;
                _tailDrawer.StartDraw();

            }
            else
            {
                _headMovementController.transform.position = position;
            }
        }

        public void GetHit()
        {
            if (_killable == false)
            {
                return;
            }
            _tailDrawer.StopDraw().Forget();
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerDeadState(this));
        }
    }
}