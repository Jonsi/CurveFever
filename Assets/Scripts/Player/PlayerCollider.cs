using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _playerBehaviour;
    
        public IPlayer Player => _playerBehaviour;
    }
}
 