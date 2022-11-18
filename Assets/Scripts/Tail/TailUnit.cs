using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Tail
{
    [RequireComponent(typeof(EdgeCollider2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class TailUnit : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private EdgeCollider2D _collider;
        private Vector2 _lastDirection = Vector2.zero;
        
        private readonly List<Vector2> _points = new List<Vector2>();

        public float Length { get; private set; } = 0;

        private void Awake()
        {
            _collider = GetComponent<EdgeCollider2D>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public Vector2 LastPoint() => _points.LastOrDefault();
        public void AddPoint(Vector2 point)
        {
            if (_points.Count == 0)
            {
                _points.Add(point);
                return;
            }
            
            Length += Vector2.Distance(point, LastPoint());

            var pointDirection = (point - LastPoint()).normalized;
            /*
            var sameDirection = Vector2.Dot(pointDirection, _lastDirection) == 1;
            if (sameDirection)
            {
                _points.Remove(LastPoint());//avoid creating unnecessary points in the same direction
            }
            */
            
            _lastDirection = pointDirection;
            
            _points.Add(point);
            SyncView();
            SyncCollider();
        }

        public void Detach()
        {
            SyncCollider(offset:0);
        }

        private void SyncView(int offset = 0)
        {
            _lineRenderer.positionCount = _points.Count;
            var syncPoints = _points.ToList();
            syncPoints.RemoveRange(_points.Count - offset, offset);
            _lineRenderer.SetPositions(syncPoints.ToArray().ToVector3Array());
        }
        
        private void SyncCollider(int offset = 1)
        {
            var syncPoints = _points.ToList();
            syncPoints.RemoveRange(_points.Count - offset, offset);
            _collider.points = syncPoints.ToArray();
        }
    }
}