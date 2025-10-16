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

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
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
        }

        // Als dit de Quit-knop is
        if (tmpText.text.ToLower().Contains("quit"))
        {
            Debug.Log("Afsluiten...");
            Application.Quit();
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
