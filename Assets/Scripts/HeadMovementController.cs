using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class HeadMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private IDisposable _flashRegistration;

    [SerializeField] private float _flickerSpeed = 1f;
    private Color _headColor;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2 MoveDirection { get; set; } = Vector2.up;

    public void Turn(TurnDirection turnDirection, float rotationSpeed)
    {
        var direction = turnDirection == TurnDirection.Right ? Vector2.right : Vector2.left;
        transform.Rotate(Vector3.forward * (-direction.x * rotationSpeed * Time.deltaTime));
    }

    public void MoveForward(float moveSpeed)
    {
        var myTransform = transform;
        _rigidBody.MovePosition(myTransform.position +(myTransform.up * (moveSpeed * Time.deltaTime)));
    }
    public void StartFlicker()
    {
        _flashRegistration = Observable.EveryUpdate().Subscribe(x=> UpdateFlashingAlpha()).AddTo(this);
    }

    public void StopFlicker()
    {
        _flashRegistration?.Dispose();
        _headColor = _spriteRenderer.color;
        var color = new Color( _headColor.r, _headColor.g, _headColor.b,1);
        _spriteRenderer.color = color;
    }
    private void UpdateFlashingAlpha()
    {
        var alpha = (Mathf.Sin(Time.time * _flickerSpeed) + 1) / 2;
        var color = _spriteRenderer.color;
        color = new Color( color.r, color.g, color.b,alpha);
        _spriteRenderer.color = color;
    }
}

public enum TurnDirection
{
    Right,
    Left
}
