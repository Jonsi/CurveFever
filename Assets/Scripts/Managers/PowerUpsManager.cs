using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class PowerUpsManager : MonoBehaviour
    {
        private PlayerManager _playerManager;

        public void Init(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}