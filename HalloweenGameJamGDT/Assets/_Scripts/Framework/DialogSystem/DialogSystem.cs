using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private DialogSystemUI dialogSystemUI;

    private DialogData[] _currentDialog;
    private Action OnDialogComplete;
    private int _currentDialogIndex;
    
    private void OnEnable()
    {
        InputReader.Instance.OnNextTextInput += OnNextTextInput;
        DialogInteractable.OnDialogInteracted += OnDialogInteracted;
    }

    private void OnDisable()
    {
        InputReader.Instance.OnNextTextInput -= OnNextTextInput;
        DialogInteractable.OnDialogInteracted -= OnDialogInteracted;
    }

    private void OnDialogInteracted(object sender, DialogInteractArgs dialogInteractArgs)
    {
        _currentDialogIndex = 0;
        _currentDialog = dialogInteractArgs.dialogDataList;
        OnDialogComplete = dialogInteractArgs.OnDialogComplete;

        dialogSystemUI.UpdateUI(GetNextDialogText().dialogText);
    }

    private void OnNextTextInput(object sender, EventArgs e)
    {
        if (_currentDialogIndex == _currentDialog.Length)
        {
            dialogSystemUI.DialogIsComplete();
            OnDialogComplete();
        }
        else
        {
            dialogSystemUI.UpdateUI(GetNextDialogText().dialogText);
        }
    }
    

    private DialogData GetNextDialogText()
    {
        var nextDialogText = _currentDialog[_currentDialogIndex];
        _currentDialogIndex++;
        return nextDialogText;
    }
}
