using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeadMovementController : MonoBehaviour
{
    public float RotationSpeed = 100f;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public Vector2 MoveDirection { get; set; } = Vector2.up;

    public void Move(Vector2 direction, float speed)
    {
        var myTransform = transform;
        _rigidBody.MovePosition(myTransform.position +(myTransform.up * (speed * Time.deltaTime)));
        transform.Rotate(Vector3.forward * (-direction.x * RotationSpeed * Time.deltaTime));
    }
}
