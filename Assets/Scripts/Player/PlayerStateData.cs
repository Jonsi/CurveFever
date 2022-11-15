using UnityEngine;

namespace Player
{
    public class PlayerStateData
    {
        public readonly float Speed;
        public readonly float RotationSpeed;
        private readonly KeyCode _left;
        private readonly KeyCode _right;

        public PlayerStateData(float speed, float rotationSpeed,KeyCode left,KeyCode right)
        {
            Speed = speed;
            RotationSpeed = rotationSpeed;
            _left = left;
            _right = right;
        }
    }
}