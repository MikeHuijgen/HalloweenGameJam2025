using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target (Speler)")]
    public Transform target;

    [Header("Camera Offset (rechter schouder)")]
    [Tooltip("X = rechts van speler, Y = hoogte, Z = achter speler")]
    public Vector3 cameraOffset = new Vector3(0.8f, 1.6f, -3f);

    [Header("Rotatie")]
    public float yaw = 0f;
    public float pitch = 10f;
    public float minPitch = -35f;
    public float maxPitch = 70f;

    [Header("Smoothing")]
    [Range(0f, 1f)] public float positionSmoothing = 0.15f;
    [Range(0f, 1f)] public float rotationSmoothing = 0.08f;

    [Header("Input")]
    public MonoBehaviour inputProviderMono;

    private ICameraInput inputProvider;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        if (inputProviderMono is ICameraInput provider)
            inputProvider = provider;
        else
            Debug.LogWarning("Geen geldige ICameraInput gevonden!");
    }

    void Start()
    {
        // Cursor lock
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (inputProvider != null)
        {
            var (dyaw, dpitch, _) = inputProvider.Sample();
            yaw += dyaw;
            pitch += dpitch;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Toggle cursor met Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !locked;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // 1️⃣ Rotatie
        Quaternion desiredRot = Quaternion.Euler(pitch, yaw, 0f);

        // 2️⃣ Pivot boven speler (borsthoogte)
        Vector3 pivot = target.position + Vector3.up * cameraOffset.y;

        // 3️⃣ Offset
        Vector3 rightOffset = target.right * cameraOffset.x;   // rechter schouder
        Vector3 backOffset = desiredRot * Vector3.forward * cameraOffset.z; // achter speler

        Vector3 desiredPos = pivot + rightOffset + backOffset;

        // 4️⃣ Smooth verplaatsing
        Vector3 smoothedPos = Vector3.Lerp(rb.position, desiredPos,
            1f - Mathf.Pow(1f - positionSmoothing, Time.fixedDeltaTime * 60f));

        rb.MovePosition(smoothedPos);

        // 5️⃣ Kijkrichting
        Quaternion desiredLook = Quaternion.LookRotation((pivot - smoothedPos).normalized, Vector3.up);
        Quaternion smoothedRot = Quaternion.Slerp(rb.rotation, desiredLook,
            1f - Mathf.Pow(1f - rotationSmoothing, Time.fixedDeltaTime * 60f));

        rb.MoveRotation(smoothedRot);
    }
}
