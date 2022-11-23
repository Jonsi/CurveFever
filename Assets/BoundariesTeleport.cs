using System;
using System.Collections.Generic;
using System.Linq;
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
        var bottomLeft = WorldArea.BottomLeft + _colliderOffset * (Vector2.down + Vector2.left);
        var topLeft = WorldArea.TopLeft + _colliderOffset * (Vector2.up + Vector2.left);
        var topRight = WorldArea.TopRight + _colliderOffset * (Vector2.up + Vector2.right);
        var bottomRight = WorldArea.BottomRight + _colliderOffset * (Vector2.down + Vector2.right);
        
        _boundaryItems.Add(CreateBoundary(topLeft, topRight, BoundariesSide.Top));
        _boundaryItems.Add(CreateBoundary(bottomLeft, bottomRight, BoundariesSide.Bottom));
        _boundaryItems.Add(CreateBoundary(bottomLeft, topLeft, BoundariesSide.Left));
        _boundaryItems.Add(CreateBoundary(topRight, bottomRight, BoundariesSide.Right));
    }

    private Boundary CreateBoundary(Vector2 pointA, Vector2 pointB, BoundariesSide side)
    {
        var boundary = Instantiate(_boundaryPrefab,transform);
        boundary.Init(new Vector2[]{pointA ,pointB} ,side);
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