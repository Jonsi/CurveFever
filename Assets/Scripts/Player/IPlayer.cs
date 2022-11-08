using UnityEngine;

namespace Player
{
    public interface IPlayer
    {
        public float GetSpeed();
        void SetSpeed(float speed);
        void Rotate(Vector2 direction);
    }
}