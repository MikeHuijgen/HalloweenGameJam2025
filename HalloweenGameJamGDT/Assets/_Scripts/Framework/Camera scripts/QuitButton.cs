using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI tmpText;
    private Color originalColor;
    public Color clickedColor = Color.red; // kleur als aangeklikt

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tmpText.color = clickedColor;
        Debug.Log("Quit button aangeklikt!");

        // Stop het spel
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // in Editor
#else
        Application.Quit(); // in build
#endif
    }
}
