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
        // Gebruik Unity legacy input � makkelijk om later te vervangen door nieuw systeem
        float yaw = InputReader.Instance.GetMouseDelta.x * sensitivity * Time.deltaTime;
        float pitch = -InputReader.Instance.GetMouseDelta.y * sensitivity * Time.deltaTime; // inverted Y off by default (negative)
        float zoom = -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        return (yaw, pitch, zoom);
    }
}
