using System;
using State.Player;
using UniRx;
using UniRx.Triggers;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HeadMovementController))]
    public class PlayerBehaviour : MonoBehaviour,IPlayer
    {
        [SerializeField] private float _moveSpeed = 1;
        
        private readonly PlayerStateMachine _stateMachine = new PlayerStateMachine();
        private HeadMovementController _headMovementController;

        private void Awake()    
        {
            _headMovementController = GetComponent<HeadMovementController>();
        }

        private void Start()
        {
            _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerMoveState(this));
            this.OnTriggerEnter2DAsObservable().Subscribe(OnHit).AddTo(this);
        }

        public float GetSpeed() => _moveSpeed;
        public void SetSpeed(float speed) => _moveSpeed = speed;
        public void Move(Vector2 direction) => _headMovementController.Move(direction,_moveSpeed);

        private void OnHit(Collider2D col)
        {
           _stateMachine.SetState(PlayerStateMachine.PlayerStateFactory.PlayerDeadState(this));
        }
    }
}