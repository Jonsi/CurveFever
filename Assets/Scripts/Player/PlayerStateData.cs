using UnityEngine;

namespace Player
{
    public class PlayerStateData
    {
        public readonly float Speed;
        public readonly float RotationSpeed;
        public readonly KeyCode LeftKey;
        public readonly KeyCode RightKey;

        public PlayerStateData(float speed, float rotationSpeed,KeyCode leftKey,KeyCode rightKey)
        {
            Speed = speed;
            RotationSpeed = rotationSpeed;
            LeftKey = leftKey;
            RightKey = rightKey;
        }
    }
}