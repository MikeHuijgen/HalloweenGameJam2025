using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Offset")]
    public Vector3 pivotOffset = new Vector3(0f, 1.6f, 0f);
    public Vector3 camOffset = new Vector3(0f, 0f, -3f);

    [Header("Rotation Settings")]
    public float yaw = 0f;
    public float pitch = 10f;
    public float minPitch = -35f;
    public float maxPitch = 70f;

    [Header("Zoom Settings")]
    public float minDistance = 1f;
    public float maxDistance = 6f;
    public float zoomSpeed = 2f;

    [Header("Collision Settings")]
    public LayerMask collisionMask = ~0;
    public float collisionRadius = 0.3f;
    public float collisionOffset = 0.2f;

    [Header("Input")]
    public MonoBehaviour inputProviderMono;

    private ICameraInput inputProvider;
    private float desiredDistance;

    void Awake()
    {
        if (inputProviderMono != null && inputProviderMono is ICameraInput)
            inputProvider = inputProviderMono as ICameraInput;
        else
            Debug.LogWarning("No ICameraInput assigned!");

        desiredDistance = camOffset.magnitude;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // --- INPUT ---
        if (inputProvider != null)
        {
            var (dyaw, dpitch, dzoom) = inputProvider.Sample();
            yaw += dyaw;
            pitch += dpitch;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            desiredDistance = Mathf.Clamp(desiredDistance + dzoom * 
                zoomSpeed, minDistance, maxDistance);
        }

        // --- ROTATION ---
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Pivot (hoogte van de speler)
        Vector3 pivot = target.position + pivotOffset;

        // Richting van camera
        Vector3 direction = rotation * Vector3.back;

        // --- COLLISION ---
        float targetDistance = desiredDistance;
        if (Physics.SphereCast(pivot, collisionRadius, 
            direction, out RaycastHit hit, 
            desiredDistance, collisionMask, 
            QueryTriggerInteraction.Ignore))
        {
            targetDistance = Mathf.Max(hit.distance - collisionOffset, minDistance);
        }

        // --- POSITIE ---
        Vector3 cameraPosition = pivot + direction * targetDistance;
        transform.position = cameraPosition;

        // Kijk altijd naar speler
        transform.rotation = Quaternion.LookRotation(pivot - cameraPosition, Vector3.up);
    }
}
