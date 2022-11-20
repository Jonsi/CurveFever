using System;
using Player;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Boundary : MonoBehaviour
{
    private EdgeCollider2D _collider;
    private BoundariesSide _side;

    public event Action<Collider2D,BoundariesSide> OnHit;

    private void Awake()
    {
        _collider = GetComponent<EdgeCollider2D>();
        _collider.isTrigger = true;
    }

    public void Init(Vector2[] points, BoundariesSide side)
    {
        _side = side;
        _collider.points = points;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnHit?.Invoke(col,_side);
    }
}