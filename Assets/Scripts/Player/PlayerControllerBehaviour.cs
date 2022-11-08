using State.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HeadMovementController))]
    public class PlayerControllerBehaviour : MonoBehaviour,IPlayerController
    {
        [Header("Settings")]
        [SerializeField] private KeyCode _leftButton;
        [SerializeField] private KeyCode _rightButton;
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _rotationSpeed = 100;
        
        private readonly PlayerStateMachine _stateMachine = new PlayerStateMachine();
        private HeadMovementController _headMovementController;

        private void Awake()    
        {
            _headMovementController = GetComponent<HeadMovementController>();
        }

        private void Start()
        {
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerMoveState(this,_leftButton,_rightButton));
            this.OnCollisionEnter2DAsObservable().Subscribe(OnHit).AddTo(this);
        }
        public float GetSpeed() => _moveSpeed;
        public void SetSpeed(float speed) => _moveSpeed = speed;
        public void Turn(TurnDirection direction) => _headMovementController.Turn(direction,_rotationSpeed);
        public void MoveForward() => _headMovementController.MoveForward(_moveSpeed);

        private void OnHit(Collision2D col)
        {
           _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerDeadState(this));
        }
    }
}