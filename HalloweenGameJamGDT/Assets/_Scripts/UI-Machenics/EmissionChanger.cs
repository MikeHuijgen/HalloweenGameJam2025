using UnityEngine;

public class EmissionChanger : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private int materialIndex = 0; // als je meerdere materials hebt
    [SerializeField] private Color emissionColor = Color.white;

    // Dit wordt aangeroepen via een knop
    public void ChangeEmissionColor()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("Geen Renderer toegewezen!");
            return;
        }

        // Veilig materiaal ophalen
        Material[] mats = targetRenderer.materials;

        if (materialIndex < 0 || materialIndex >= mats.Length)
        {
            Debug.LogError("Material index buiten bereik!");
            return;
        }

        Material mat = mats[materialIndex];

        // emissie aanzetten en kleur toepassen
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor);
    }

    // Optioneel: hiermee kun je de kleur dynamisch instellen
    public void SetEmissionColor(Color newColor)
    {
        emissionColor = newColor;
    }
}
