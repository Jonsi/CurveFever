using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraBoundsCollider : MonoBehaviour
{
    private BoxCollider2D _camBoundsCollider;
    private EdgeCollider2D _edgeCollider;
    private void Awake()
    {
        _camBoundsCollider = GetComponent<BoxCollider2D>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
    }
    private void Start()
    {
        var bounds = Utils.CameraUtils.GetCameraBoundariesSize(Camera.main);
        var xOffset = bounds.x / 2;
        var yOffset = bounds.y / 2;
        
        var colliderPoints = new Vector2[]
        {
            Vector2.left * xOffset + Vector2.down * yOffset,
            Vector2.left * xOffset + Vector2.up * yOffset,
            Vector2.right * xOffset + Vector2.up * yOffset,
            Vector2.right * xOffset + Vector2.down * yOffset,
            Vector2.left * xOffset + Vector2.down * yOffset,
        };
        _edgeCollider.points = colliderPoints;
    }
}
