using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TMPButton : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI tmpText;
    private Color originalColor;
    public Color clickedColor = Color.yellow;
    private bool isClicked = false;

    [Header("CameraMover (optioneel)")]
    public CameraMover cameraMover;

    [Header("UI Elementen die moeten verdwijnen bij 'Play'")]
    public GameObject playText;
    public GameObject quitText;

    [Header("Emission Controller (voor schermkleur)")]
    public EmissionChanger emissionChanger;
    public Color defaultColor = Color.yellow; // standaard geel

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;

        // Zet schermkleur bij start meteen op geel
        if (emissionChanger != null)
        {
            emissionChanger.SetEmissionColor(defaultColor);
            emissionChanger.ChangeEmissionColor();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Button '{tmpText.text}' is aangeklikt!");

        // Als dit de Play-knop is
        if (tmpText.text.ToLower().Contains("play"))
        {
            // Verberg de Play- en Quit-teksten
            if (playText != null) playText.SetActive(false);
            if (quitText != null) quitText.SetActive(false);

            // Zet scherm terug naar geel
            if (emissionChanger != null)
            {
                emissionChanger.SetEmissionColor(defaultColor);
                emissionChanger.ChangeEmissionColor();
            }
        }

        // Als dit de Quit-knop is
        if (tmpText.text.ToLower().Contains("quit"))
        {
            Debug.Log("Afsluiten...");
            Application.Quit();

            // Ook bij quit: kleur terug naar geel
            if (emissionChanger != null)
            {
                emissionChanger.SetEmissionColor(defaultColor);
                emissionChanger.ChangeEmissionColor();
            }
        }

        // Camera bewegen als cameraMover is toegewezen
        if (cameraMover != null)
        {
            cameraMover.MoveCamera();
        }

        // Eventueel visuele klikfeedback
        isClicked = !isClicked;
        tmpText.color = isClicked ? clickedColor : originalColor;
    }
}
