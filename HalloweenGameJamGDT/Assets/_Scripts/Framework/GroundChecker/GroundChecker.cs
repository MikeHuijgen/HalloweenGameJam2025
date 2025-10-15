using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isGrounded = true;

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }

    public bool IsPlayerGrounded => _isGrounded;
}
