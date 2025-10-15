using UnityEngine;

public class EmissionButton : MonoBehaviour
{
    [Header("Verwijzing naar de color cycler")]
    public EmissionColorCycler colorCycler;

    [Header("True = volgende kleur, False = vorige kleur")]
    public bool nextButton = true;

    private void OnMouseDown()
    {
        if (colorCycler == null) return;

        if (nextButton)
            colorCycler.NextColor();
        else
            colorCycler.PreviousColor();
    }
}
