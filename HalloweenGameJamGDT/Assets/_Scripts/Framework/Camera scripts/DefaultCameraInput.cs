// DefaultCameraInput.cs
using UnityEngine;

public class DefaultCameraInput : MonoBehaviour, ICameraInput
{
    [Tooltip("Mouse sensitivity multiplier")]
    public float sensitivity = 1f;

    [Tooltip("Scroll wheel zoom sensitivity")]
    public float zoomSensitivity = 2f;

    public (float yaw, float pitch, float zoom) Sample()
    {
        // Gebruik Unity legacy input — makkelijk om later te vervangen door nieuw systeem
        float yaw = Input.GetAxis("Mouse X") * sensitivity;
        float pitch = -Input.GetAxis("Mouse Y") * sensitivity; // inverted Y off by default (negative)
        float zoom = -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        return (yaw, pitch, zoom);
    }
}
