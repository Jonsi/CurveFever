using UnityEngine;

namespace Player
{
    public interface IPlayerController
    {
        public float GetSpeed();
        void SetSpeed(float speed);
        void Turn(TurnDirection direction);
        void MoveForward();
    }
}