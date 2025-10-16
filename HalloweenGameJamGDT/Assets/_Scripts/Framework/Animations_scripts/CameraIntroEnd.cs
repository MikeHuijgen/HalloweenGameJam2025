using UnityEngine;

public class CameraIntroEnd : MonoBehaviour
{
    [Header("Verwijzingen")]
    public GameObject introCamera;
    public GameObject mainCamera;

    // Zorg dat de naam precies zo is als hieronder
    public void OnIntroAnimationEnd()
    {
        if (mainCamera != null)
            mainCamera.SetActive(true);

        if (introCamera != null)
            introCamera.SetActive(false);
        // of: Destroy(introCamera);
    }
}
