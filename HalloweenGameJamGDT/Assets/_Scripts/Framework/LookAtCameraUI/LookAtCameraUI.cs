using UnityEngine;

public class LookAtCameraUI : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        var directionToCamera = (_cameraTransform.position - transform.position).normalized;
        transform.LookAt(transform.position + directionToCamera * -1);
    }
}
