using System.Collections.Generic;
using System.Linq;
using PowerBall;
using UnityEngine;

public class PowerBallGenerator : MonoBehaviour
{
    [SerializeField] private PowerBallBehaviour _powerBallPrefab;

    [SerializeField] private List<PowerBall.PowerBall> _powerBalls;
    [SerializeField] private float _creationEdgeOffset = 0.1f;

    private void Start()
    {
        var powerBallSettings = _powerBalls.FirstOrDefault();
        var position = Utils.WorldArea.RandomWorldPosition(_creationEdgeOffset);
        var instance = Instantiate(_powerBallPrefab, position, Quaternion.identity, transform);
        instance.InitializeUsingSettings(powerBallSettings);
    }
}
