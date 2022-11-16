using Player;
using UnityEngine;

namespace PowerBall
{
    
    public abstract class PowerBall : ScriptableObject
    {
        public Sprite Sprite;
        public abstract Color GetColor();
        public abstract void ApplyPower(IPlayer hitPlayer);
        public abstract void UnApplyPower(IPlayer hitTPlayer);
    }
}
