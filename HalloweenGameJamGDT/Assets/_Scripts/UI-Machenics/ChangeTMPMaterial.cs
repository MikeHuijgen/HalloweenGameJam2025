using TMPro;
using UnityEngine;

public class ChangeTMPMaterial : MonoBehaviour
{
    public TMP_Text tmpText;
    public Material newMaterial;

    void Start()
    {
        if (tmpText != null && newMaterial != null)
        {
            tmpText.fontSharedMaterial = newMaterial;
        }
    }
}
