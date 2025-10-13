using System;
using UnityEngine;

public class DialogInteractable : Interactable
{
    [SerializeField] private DialogData[] dialog;

    public static EventHandler<DialogInteractArgs> OnDialogInteracted;

    public override void Interact(Action action)
    {
        OnInteractComplete = action;
        InputReader.Instance.SwitchToDialogActionMap();
        OnDialogInteracted?.Invoke(this, new DialogInteractArgs(dialog, OnDialogComplete));
    }

    private void OnDialogComplete()
    {
        InputReader.Instance.SwitchToGameplayActionMap();
        OnInteractComplete();     
    }
}
