using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // speler

    [Header("Offset & Distance")]
    public Vector3 offset = new Vector3(0.5f, 1.6f, -3f);
    public float minDistance = 1f;
    public float maxDistance = 6f;

    [Header("Rotation")]
    public float yaw = 0f;
    public float pitch = 10f;
    public float minPitch = -35f;
    public float maxPitch = 70f;

    [Header("Smoothing")]
    [Range(0f, 1f)] public float positionSmoothing = 0.12f;
    [Range(0f, 1f)] public float rotationSmoothing = 0.08f;
    public float zoomSmoothing = 0.1f;

    [Header("Collision")]
    public LayerMask collisionMask = ~0;
    public float collisionRadius = 0.25f;
    public float collisionOffset = 0.1f;
    public float collisionSmooth = 10f;

    [Header("Input")]
    public MonoBehaviour inputProviderMono; // assign DefaultCameraInput

    private ICameraInput inputProvider;
    private Rigidbody rb;
    private float targetDistance;
    private float currentDistance;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        if (inputProviderMono != null && inputProviderMono is ICameraInput)
            inputProvider = inputProviderMono as ICameraInput;
        else
            Debug.LogWarning("No ICameraInput assigned!");

        targetDistance = offset.magnitude;
        currentDistance = targetDistance;
    }

    void Start()
    {
        Vector3 e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;
    }

    void Update()
    {
        if (inputProvider != null)
        {
            var (dyaw, dpitch, dzoom) = inputProvider.Sample();
            yaw += dyaw;
            pitch += dpitch;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            targetDistance = Mathf.Clamp(targetDistance + dzoom, minDistance, maxDistance);
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Rotatie berekenen
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Gewenste positie vóór collision
        Vector3 desiredPosition = target.position + rotation * offset.normalized * targetDistance;

        // Collision check: van target naar gewenste camera
        Vector3 direction = desiredPosition - (target.position + Vector3.up * 1.4f);
        float distance = direction.magnitude;

        RaycastHit hit;
        float finalDistance = targetDistance;

        if (Physics.SphereCast(target.position + Vector3.up * 1.4f, collisionRadius, direction.normalized, out hit, distance, collisionMask))
        {
            finalDistance = Mathf.Clamp(hit.distance - collisionOffset, minDistance, targetDistance);
        }

        // Smooth collision afstand
        currentDistance = Mathf.Lerp(currentDistance, finalDistance, Time.fixedDeltaTime * collisionSmooth);

        // Final positie
        Vector3 finalPosition = target.position + rotation * offset.normalized * currentDistance;

        // Smooth beweging
        rb.MovePosition(Vector3.Lerp(rb.position, finalPosition, 1f - Mathf.Pow(1f - positionSmoothing, Time.fixedDeltaTime * 60f)));

        // Camera kijkt altijd naar speler
        Vector3 lookTarget = target.position + Vector3.up * 1.4f;
        Quaternion desiredRot = Quaternion.LookRotation(lookTarget - rb.position, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, desiredRot, 1f - Mathf.Pow(1f - rotationSmoothing, Time.fixedDeltaTime * 60f)));
    }
}
