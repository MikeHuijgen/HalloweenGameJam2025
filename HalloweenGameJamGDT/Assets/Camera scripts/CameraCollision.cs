// CameraCollision.cs
/*using UnityEngine;

[RequireComponent(typeof(ThirdPersonCamera))]
public class CameraCollision : MonoBehaviour
{
    public LayerMask collisionMask = ~0; // everything by default
    [Tooltip("Hoe snel de camera terugveert als er een obstakel is")]
    public float collisionSmooth = 10f;
    [Tooltip("Min afstand wanneer maximaal ingekort")]
    public float minCollisionDistance = 0.5f;

    ThirdPersonCamera third;
    float currentDistance;

    void Awake()
    {
        third = GetComponent<ThirdPersonCamera>();
        currentDistance = third != null ? third.localOffset.magnitude : 3f;
    }

    void LateUpdate()
    {
        if (third == null || third.target == null) return;

        // Compute desired world offset like in ThirdPersonCamera
        Quaternion rot = Quaternion.Euler(third.pitch, third.yaw, 0f);
        Vector3 dir = rot * third.localOffset.normalized; // direction from target to camera
        float desired = third.localOffset.magnitude; // desired distance

        // Raycast from target toward desired camera pos
        Vector3 origin = third.target.position + Vector3.up * 1.4f; // head height
        RaycastHit hit;
        float hitDistance = desired;

        if (Physics.SphereCast(origin, 0.25f, dir, out hit, desired, collisionMask, QueryTriggerInteraction.Ignore))
        {
            hitDistance = Mathf.Max(hit.distance - 0.1f, minCollisionDistance); // small offset
        }

        // Smoothly apply collision distance
        currentDistance = Mathf.Lerp(currentDistance, hitDistance, Time.deltaTime * collisionSmooth);

        // Update ThirdPersonCamera's targetDistance indirectly by setting localOffset magnitude
        // Keep direction, but scale localOffset for camera's desired distance
        // (we only want to affect runtime effective distance, so call a setter or modify internal variable)
        // Simpler: we modify third's targetDistance via reflection-like approach? Instead we set third.localOffset to same dir * currentDistance
        Vector3 newLocalOffset = third.localOffset.normalized * currentDistance;
        third.localOffset = newLocalOffset;
    }
}*/
