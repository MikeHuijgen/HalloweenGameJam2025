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

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle kleur feedback
        isClicked = !isClicked;
        tmpText.color = isClicked ? clickedColor : originalColor;

        Debug.Log($"Button '{tmpText.text}' is aangeklikt!");

        // Camera bewegen als cameraMover is toegewezen
        if (cameraMover != null)
        {
            cameraMover.MoveCamera();
        }
    }
}
