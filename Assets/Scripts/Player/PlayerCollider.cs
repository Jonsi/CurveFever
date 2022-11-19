using DefaultNamespace;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollider : MonoBehaviour,IHittable
    {
        [SerializeField] private PlayerBehaviour _playerBehaviour;
    
        public IPlayer Player => _playerBehaviour;
        public void GetHit()
        {
            _playerBehaviour.GetHit();
        }
    }
}
 