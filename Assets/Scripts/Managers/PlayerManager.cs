using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Player;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [SerializeField] private VoidGameEvent _gameStartedEvent;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            _gameStartedEvent.RegisterListener(OnGameStarted);
        }

        private void OnGameStarted()
        {
            _alivePlayers = _allPlayers.ToList();
        }

        private readonly List<IPlayer> _allPlayers = new List<IPlayer>();
        private List<IPlayer> _alivePlayers;
        
        public List<IPlayer> GetAlivePlayers()
        {
            return _alivePlayers.ToList();
        }

        public void RegisterPlayer(IPlayer player)
        {
            _allPlayers.Add(player);
        }

        public void UnRegisterSession(IPlayer player)
        {
            _allPlayers.Remove(player);
        }
    }
}