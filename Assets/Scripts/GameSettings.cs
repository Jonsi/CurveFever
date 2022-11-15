using System.Collections.Generic;
using Player;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    private const float MaxPlayers = 4;
    
    public PlayerBehaviour PlayerPrefab;
    [Range(1, MaxPlayers)] public int PlayerCount = 1;
    public List<PlayerSettings> PlayerSettings = new List<PlayerSettings>();
    [Range(0, 1)] public float SpawnAreaOffset = 0.1f; 
}