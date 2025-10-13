using System;
using TMPro;
using UnityEngine;

public class DialogSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogSystemUICanvas;
    [SerializeField] private TextMeshProUGUI dialogText;

    private void Awake() => SetDialogCanvasUI(false);

    public void UpdateUI(string dialogText)
    {
        if (!dialogSystemUICanvas.activeInHierarchy) SetDialogCanvasUI(true);

        this.dialogText.text = dialogText;
    }

    public void DialogIsComplete() => SetDialogCanvasUI(false);

    private void SetDialogCanvasUI(bool value) => dialogSystemUICanvas.SetActive(value);
}
