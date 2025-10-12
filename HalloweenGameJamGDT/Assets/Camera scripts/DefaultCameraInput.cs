using UnityEngine;

public class DefaultCameraInput : MonoBehaviour, ICameraInput
{
    [Header("Mouse Settings")]
    public float sensitivity = 2f;
    public float zoomSensitivity = 2f;

    public (float yaw, float pitch, float zoom) Sample()
    {
        float yaw = Input.GetAxis("Mouse X") * sensitivity;
        float pitch = -Input.GetAxis("Mouse Y") * sensitivity; // inverted Y
        float zoom = -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        return (yaw, pitch, zoom);
    }
}
