using Player;
using UnityEngine;

namespace PowerBall
{
    
    public abstract class PowerBall : ScriptableObject
    {
        public Sprite Sprite;

        public virtual Color GetColor() => Color.white;
        public abstract void ApplyPower(IPlayer hitPlayer);
        public abstract void UnApplyPower(IPlayer hitTPlayer);
    }
}
