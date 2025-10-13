using System;
using TMPro;
using UnityEngine;

public class DialogSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogSystemUICanvas;
    [SerializeField] private TextMeshProUGUI speakersNameText;
    [SerializeField] private TextMeshProUGUI dialogText;

    private void Awake() => SetDialogCanvasUI(false);

    public void UpdateUI(string dialogText, string speakersName)
    {
        if (!dialogSystemUICanvas.activeInHierarchy) SetDialogCanvasUI(true);

        this.dialogText.text = dialogText;
        this.speakersNameText.text = speakersName;
    }

    public void DialogIsComplete() => SetDialogCanvasUI(false);

    private void SetDialogCanvasUI(bool value) => dialogSystemUICanvas.SetActive(value);
}
