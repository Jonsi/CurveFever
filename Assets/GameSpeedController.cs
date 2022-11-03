using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    [Range(0,2)]
    [SerializeField] private float _gameSpeed = 1f;

    void Update()
    {
        Time.timeScale = _gameSpeed;
    }
}
