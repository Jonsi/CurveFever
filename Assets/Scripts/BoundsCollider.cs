using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Events;
using UniRx.Triggers;
using UnityEngine;
using Utils;

[RequireComponent(typeof(EdgeCollider2D))]
public class BoundsCollider : MonoBehaviour
{
    private BoxCollider2D _camBoundsCollider;
    private EdgeCollider2D _edgeCollider;

    [SerializeField] private VoidGameEvent _gameInitializedEvent;
    private void Awake()
    {
        _camBoundsCollider = GetComponent<BoxCollider2D>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnEnable()
    {
        _gameInitializedEvent.RegisterListener(SetBorders);
    }

    private void OnDisable()
    {
        _gameInitializedEvent.UnRegisterListener(SetBorders);
    }

    private void SetBorders()
    {
        var colliderPoints = new Vector2[]
        {
            WorldArea.BottomLeft,
            WorldArea.TopLeft,
            WorldArea.TopRight,
            WorldArea.BottomRight,
            WorldArea.BottomLeft,
        };
        _edgeCollider.points = colliderPoints;
    }
}
