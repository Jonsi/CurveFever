using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public enum BoundariesSide
{
    Top,
    Bottom,
    Left,
    Right
}

public class BoundariesTeleport : MonoBehaviour
{
    [SerializeField] private float _colliderOffset;
    [SerializeField] private VoidGameEvent _gameInitialized;
    [SerializeField] private Boundary _boundaryPrefab;

    private readonly List<Boundary> _boundaryItems = new List<Boundary>();
    
    private void OnEnable()
    {
        _gameInitialized.RegisterListener(OnGameInitialized);
    }

    private void OnDisable()
    {
        _gameInitialized.UnRegisterListener(OnGameInitialized);
    }

    private void OnGameInitialized()
    {
        _boundaryItems.Add(CreateBoundary(WorldArea.TopLeft, WorldArea.TopRight, BoundariesSide.Top));
        _boundaryItems.Add(CreateBoundary(WorldArea.BottomLeft, WorldArea.BottomRight, BoundariesSide.Bottom));
        _boundaryItems.Add(CreateBoundary(WorldArea.BottomLeft, WorldArea.TopLeft, BoundariesSide.Left));
        _boundaryItems.Add(CreateBoundary(WorldArea.TopRight, WorldArea.BottomRight, BoundariesSide.Right));
    }

    private Boundary CreateBoundary(Vector2 pointA, Vector2 pointB, BoundariesSide side)
    {
        var boundary = Instantiate(_boundaryPrefab,transform);
        boundary.Init(new Vector2[]{pointA,pointB},side);
        boundary.OnHit += OnBoundaryCollision;
        return boundary;
    }
    
    private void OnBoundaryCollision(Collider2D col,BoundariesSide side)
    {
        if (col.TryGetComponent(out ITeleport teleportAble) == false)
        {
            return;
        }

        var collisionPoint = col.transform.position;
        Vector2 newPos;
        float flippedPoint;
        switch (side)
        {
            case BoundariesSide.Top:
                flippedPoint = collisionPoint.y * -1 + _colliderOffset;
                newPos = new Vector2(collisionPoint.x, flippedPoint);
                break;
            case BoundariesSide.Bottom:
                flippedPoint = collisionPoint.y * -1 - _colliderOffset;
                newPos = new Vector2(collisionPoint.x, flippedPoint);
                break;
            case BoundariesSide.Right:
                flippedPoint = collisionPoint.x * -1 + _colliderOffset;
                newPos = new Vector2(flippedPoint, collisionPoint.y);
                break;
            case BoundariesSide.Left:
                flippedPoint = collisionPoint.x * -1 - _colliderOffset;
                newPos = new Vector2(flippedPoint, collisionPoint.y);
                break;
 
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        teleportAble.Teleport(newPos);
    }
}