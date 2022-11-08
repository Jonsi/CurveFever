using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeadMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
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
}

public enum TurnDirection
{
    Right,
    Left
}
