using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public float StartSpeed;
        public float StartRotationSpeed;
        public KeyCode LeftKey;
        public KeyCode RightKey;
        public Color HeadColor;
    }
}