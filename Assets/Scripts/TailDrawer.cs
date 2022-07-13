using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TailDrawer : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;

    public float pointSpacing = 0.1f;
    [Range(0,1f)]public float colliderPointOffset = 0.1f;

    private float _lastRotation;
    private List<Vector2> _points = new List<Vector2>();

    private void Awake()
    {
        SetPoint(_head.position,true);
        //SetPoint(_head.position,true);
        _lastRotation = _head.rotation.z + 1;
    }

    private void Update()
    {
        Vector2 lastPos = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        if (Vector2.Distance(lastPos, _head.position) > pointSpacing)
        {
            SetPoint(_head.position);
        }
    }

    public void SetPoint(Vector2 point, bool asNew = false)
    {
        if (asNew || _head.rotation.z != _lastRotation)
        {
            _points.Add(point);
            _lineRenderer.positionCount++;
            _lastRotation = _head.rotation.z;
        }
        else
        {
            _points[_points.Count - 1] = point;
        }

        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _points.Last());

        if(_points.Count > 1)
        {
            _points[_points.Count - 1] = _points.Last() + (_points[_points.Count - 2] - _points.Last()).normalized * colliderPointOffset;
            _collider.points = _points.ToArray();
        }

    }
}
