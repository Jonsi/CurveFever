using Events;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private VoidGameEvent _gameInitializedEvent;
        [SerializeField] private Transform _playersHolder;
        [SerializeField] private GameSettings _gameSettings;

        private void InstantiatePlayers()
        {
            for (var i = 0; i < _gameSettings.PlayerCount; i++)
            {
                var player = Instantiate(_gameSettings.PlayerPrefab, _playersHolder);
                player.InitializeUsingSettings(_gameSettings.PlayerSettings[i]);
                player.Teleport(WorldArea.RandomWorldPosition(_gameSettings.SpawnAreaOffset));
                PlayerManager.Instance.RegisterPlayer(player);
            }
        }
        private void Start()
        {
            GameFlow();
        }

        private  void GameFlow()
        {
            WorldArea.Initialize(CameraUtils.GetCameraBoundariesSize(_camera ? _camera : Camera.main));
            InstantiatePlayers();
            _gameInitializedEvent.Invoke();
        }
    }
}
