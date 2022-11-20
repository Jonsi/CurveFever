using System;
using DefaultNamespace;
using Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class BoundariesController : MonoBehaviour
{
    public static BoundariesController Instance;

    [SerializeField] private float _boundariesColliderOffset;
    [SerializeField] private float _boundariesLineOffset;
    [SerializeField] private float _flashingSpeed = 1;
    [SerializeField] private VoidGameEvent _gameInitializedEvent;

    private EdgeCollider2D _collider;
    private LineRenderer _lineRenderer;
    private IDisposable _flashRegistration;
    private void Awake()
    {
        Instance = this;
        _collider = GetComponent<EdgeCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        _gameInitializedEvent.RegisterListener(CreateBoundaries);
    }

    private void OnDisable()
    {
        _gameInitializedEvent.UnRegisterListener(CreateBoundaries);
    }

    private void CreateBoundaries()
    {
        GenerateCollider();
        DrawBorders();
    }
    
    private void GenerateCollider()
    {
        _collider.points = WorldArea.GetWorldAreaAsFramePoints(_boundariesColliderOffset);
    }
    
    private void DrawBorders()
    {
        var colliderPoints = WorldArea.GetWorldAreaAsFramePoints(_boundariesLineOffset);
        _lineRenderer.positionCount = colliderPoints.Length;
        _lineRenderer.SetPositions(colliderPoints.ToVector3Array());;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IHittable hittable))
        {
            hittable.GetHit();
        }
    }

    public void DisableBoundaries()
    {
        _flashRegistration = this.UpdateAsObservable().Subscribe(x=> UpdateFlashingAlpha()).AddTo(this);
        _collider.enabled = false;
    }

    public void EnableBoundaries()
    {
        _flashRegistration?.Dispose();
        _collider.enabled = true;
        var startColor = _lineRenderer.startColor;
        startColor = new Color( startColor.r, startColor.g, startColor.b,1);
        _lineRenderer.startColor = startColor;
    }

    private void UpdateFlashingAlpha()
    {
        var alpha = (Mathf.Sin(Time.time * _flashingSpeed) + 1) / 2;
        var startColor = _lineRenderer.startColor;
        startColor = new Color( startColor.r, startColor.g, startColor.b,alpha);
        _lineRenderer.startColor = startColor;
    }
}
