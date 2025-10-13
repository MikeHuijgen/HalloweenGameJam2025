using UnityEngine;

public class MouseCursorLocker : MonoBehaviour
{
    public void LockCursor() => Cursor.lockState = CursorLockMode.Locked;
    public void UnLockCursor() => Cursor.lockState = CursorLockMode.None;
}
