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
        [SerializeField] private AutoTailDrawer tailDrawer;

        [Header("Events")] [SerializeField] private VoidGameEvent _gameStartedEvent;
        
        private KeyCode _leftButton;
        private KeyCode _rightButton;
        private float _moveSpeed = 1;
        private float _rotationSpeed = 100;
        
        private readonly PlayerStateMachine _stateMachine = new PlayerStateMachine();
        
        private void OnGameStarted()
        {
            _headMovementController.transform.RotateTowards(Vector2.zero);
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerMoveState(this,_leftButton,_rightButton));
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
        }

        private void OnDisable()
        {
            _gameStartedEvent.UnRegisterListener(OnGameStarted);
        }

        public float GetSpeed() => _moveSpeed;
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

        public PlayerStateData GetStateData()
        {
            return new PlayerStateData(_moveSpeed, _rotationSpeed,_leftButton,_rightButton);
        }

        public void ApplyStateData(PlayerStateData playerStateData)
        {
            _moveSpeed = playerStateData.Speed;
            _rotationSpeed = playerStateData.RotationSpeed;
            _leftButton = playerStateData.LeftKey;
            _rightButton = playerStateData.RightKey;
        }

        public async void Teleport(Vector2 position)
        {
            if (WorldArea.IsInsidePerimeters(position) == false)
            {
                return;
            }
            
            await tailDrawer.StopDraw();
            _headMovementController.transform.position = position;
            tailDrawer.StartDraw();
        }

        public void GetHit()
        {
            tailDrawer.StopDraw().Forget();
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerDeadState(this));
        }
    }
}