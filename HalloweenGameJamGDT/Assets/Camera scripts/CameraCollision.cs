using UnityEngine;

[RequireComponent(typeof(ThirdPersonCamera))]
public class CameraCollision : MonoBehaviour
{
    public LayerMask collisionMask = ~0;
    public float sphereRadius = 0.3f;
    public float minDistance = 0.5f;
    public float smoothSpeed = 10f;

    [Tooltip("Minimale hoogte van camera boven de grond")]
    public float minHeightAboveGround = 0.5f;

    private ThirdPersonCamera cam;
    private float currentZ;

    void Awake()
    {
        cam = GetComponent<ThirdPersonCamera>();
        currentZ = cam.cameraOffset.z;
    }

    void LateUpdate()
    {
        if (cam.target == null) return;

        Vector3 targetPos = cam.target.position + Vector3.up * cam.cameraOffset.y;
        Vector3 rightOffset = cam.target.right * cam.cameraOffset.x;

        Quaternion rot = Quaternion.Euler(cam.pitch, cam.yaw, 0f);
        Vector3 dir = rot * Vector3.forward;
        float desiredDist = Mathf.Abs(cam.cameraOffset.z);

        Vector3 desiredPos = targetPos + rightOffset + dir * cam.cameraOffset.z;

        // 1️⃣ SphereCast achter de speler (Z) - voorkomt muren
        Ray ray = new Ray(targetPos + rightOffset, dir);
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, desiredDist, collisionMask))
        {
            float hitDist = Mathf.Max(hit.distance - 0.1f, minDistance);
            currentZ = -Mathf.Lerp(-currentZ, hitDist, Time.deltaTime * smoothSpeed);
        }
        else
        {
            currentZ = Mathf.Lerp(currentZ, cam.cameraOffset.z, Time.deltaTime * smoothSpeed);
        }

        // Update Z-offset
        var newOffset = cam.cameraOffset;
        newOffset.z = currentZ;

        // 2️⃣ Raycast naar beneden vanaf de nieuwe positie (Y-collision)
        Vector3 checkPos = targetPos + rightOffset + rot * Vector3.forward * currentZ;
        if (Physics.Raycast(checkPos, Vector3.down, out RaycastHit groundHit, Mathf.Infinity, collisionMask))
        {
            float minY = groundHit.point.y + minHeightAboveGround;
            if (checkPos.y < minY)
            {
                // Verhoog camera zodat hij niet door de grond gaat
                checkPos.y = minY;
            }
        }

        // Pas cameraOffset aan zodat FixedUpdate het kan gebruiken
        cam.cameraOffset = new Vector3(newOffset.x, checkPos.y - cam.target.position.y, newOffset.z);
    }
}
