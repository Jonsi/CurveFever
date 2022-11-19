using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

[RequireComponent(typeof(LineRenderer))]
public class BoundariesDrawer : MonoBehaviour
{
    [SerializeField] private VoidGameEvent _gameInitializedEvent;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        DrawBorders();
    }

    private void OnEnable()
    {
        _gameInitializedEvent.RegisterListener(DrawBorders);
    }

    private void OnDisable()
    {
        _gameInitializedEvent.UnRegisterListener(DrawBorders);
    }

    private void DrawBorders()
    {
        var colliderPoints = new Vector3[]
        {
            WorldArea.BottomLeft,
            WorldArea.TopLeft,
            WorldArea.TopRight,
            WorldArea.BottomRight,
            WorldArea.BottomLeft,
        };
        _lineRenderer.positionCount = colliderPoints.Length;
        _lineRenderer.SetPositions(colliderPoints);;
    }
}
