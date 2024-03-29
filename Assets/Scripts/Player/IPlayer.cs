﻿using UnityEngine;

namespace Player
{
    public interface IPlayer
    {
        public float GetSpeed();
        public void Scale(float multiplier);
        public float GetRotationSpeed();
        void SetSpeed(float speed);
        void SetRotationSpeed(float speed);
        void Turn(TurnDirection direction);
        void MoveForward();
        void InvertDirectionInput();
        void SetKillable(bool killable);
    }
}