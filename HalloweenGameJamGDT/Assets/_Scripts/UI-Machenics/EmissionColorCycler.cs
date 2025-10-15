using UnityEngine;

public class EmissionColorCycler : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private int materialIndex = 0;

    [SerializeField] private Color[] colors;  // lijst met kleuren in de Inspector
    private int currentColorIndex = 0;

    private void Start()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("Geen Renderer toegewezen!");
            return;
        }

        if (colors.Length == 0)
        {
            // geef een paar standaardkleuren
            colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
        }

        ApplyEmissionColor(colors[currentColorIndex]);
    }

    public void NextColor()
    {
        currentColorIndex++;
        if (currentColorIndex >= colors.Length)
            currentColorIndex = 0;

        ApplyEmissionColor(colors[currentColorIndex]);
    }

    public void PreviousColor()
    {
        currentColorIndex--;
        if (currentColorIndex < 0)
            currentColorIndex = colors.Length - 1;

        ApplyEmissionColor(colors[currentColorIndex]);
    }

    private void ApplyEmissionColor(Color newColor)
    {
        Material[] mats = targetRenderer.materials;

        if (materialIndex < 0 || materialIndex >= mats.Length)
        {
            Debug.LogError("Material index buiten bereik!");
            return;
        }

        Material mat = mats[materialIndex];
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", newColor);
    }
}
