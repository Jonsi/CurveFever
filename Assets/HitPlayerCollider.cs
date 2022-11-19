using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitPlayerCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IHittable hittable))
        {
            hittable.GetHit();
        }
    }
}
