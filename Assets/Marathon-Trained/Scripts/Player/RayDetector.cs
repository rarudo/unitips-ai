using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Raycastを打って当たったら教えてくれるクラス
/// </summary>
public class RayDetector : MonoBehaviour
{
    [SerializeField] private Vector3 rayDirection;
    [SerializeField] private float rayDistance;
    
    public RaycastHitEvent OnRaycastHit;
    public float LastHitDistance { get; private set; }
    public RaycastHit2D LastHitInfo { get; private set; }

    [SerializeField] private bool drawDebugLine = false;

    [System.Serializable]
    public class RaycastHitEvent : UnityEvent<RaycastHit2D>
    {
    }

    private void Awake()
    {
        OnRaycastHit = new RaycastHitEvent();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance);
        var isHit = hit.collider != null;
        if (isHit)
        {
            OnRaycastHit.Invoke(hit);
            LastHitDistance = hit.distance;
            LastHitInfo = hit;
        }

        if (drawDebugLine) {
            if (isHit) {
                Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);
            } else {
                Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.blue);
            }
        }
    }
}
