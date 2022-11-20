using DefaultNamespace;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollisionHandler : MonoBehaviour,IHittable,ITeleport
    {
        [SerializeField] private PlayerBehaviour _playerBehaviour;
        public IPlayer Player => _playerBehaviour;
        public void GetHit()
        {
            _playerBehaviour.GetHit();
        }

        public void Teleport(Vector2 position)
        {
            _playerBehaviour.Teleport(position);
        }
    }
    
}
 