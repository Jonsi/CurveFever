using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utils;

public class TailDrawer : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;

    public float PointSpacing = 0.1f;
    private readonly List<Vector3> _points = new List<Vector3>();

    private void Awake()
    {
        AddNewPoint(_head.position);
    }

    private void Update()
    {
        SyncTailPoints();
        SyncView();
        SyncCollider();
    }

    private void SyncTailPoints()
    {
        var lastRenderedPos = _points.Last();
        if (Vector2.Distance(lastRenderedPos, _head.position) > PointSpacing)
        {
            AddNewPoint(_head.position);
        }
    }
    
    private void SyncView()
    {
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }

    private void SyncCollider()
    {
        var points = _points.ToList();
        points.Remove(points.Last());   //create offset to avoid self collision at creation
        _collider.points = points.ToArray().ToVector2Array();
    }

    private void AddNewPoint(Vector2 point)
    {
        _points.Add(point);
    }
}
