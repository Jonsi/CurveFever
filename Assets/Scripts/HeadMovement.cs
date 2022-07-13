using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float RotationSpeed = 100f;

    public Vector2 MoveDirection { get; set; } = Vector2.up;

    private float _horizontal = 0;

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * -_horizontal * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
