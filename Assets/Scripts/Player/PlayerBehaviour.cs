using System;
using Events;
using State.Player;
using Tail;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour,IPlayer
    {
        [Header("Components")]
        [SerializeField] private HeadMovementController _headMovementController;
        [SerializeField] private PlayerTailDrawer _tailDrawer;

        [Header("Events")] [SerializeField] private VoidGameEvent _gameStartedEvent;
        
        private KeyCode _leftButton;
        private KeyCode _rightButton;
        private float _moveSpeed = 1;
        private float _rotationSpeed = 100;
        
        private readonly PlayerStateMachine _stateMachine = new PlayerStateMachine();

        private void Awake()
        {
            _headMovementController.OnCollisionEnter2DAsObservable().Subscribe(OnHit).AddTo(this);
        }
        
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
        public PlayerStateData GetStateData()
        {
            return new PlayerStateData(_moveSpeed, _rotationSpeed,_leftButton,_rightButton);
        }

        public void Teleport(Vector2 position)
        {
            if (WorldArea.IsInsidePerimeters(position) == false)
            {
                return;
            }
            
            _tailDrawer.StopDraw();
            _headMovementController.transform.position = position;
            _tailDrawer.StartDraw();
        }
        
        private void OnHit(Collision2D col)
        {
           _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerDeadState(this));
        }
    }
}