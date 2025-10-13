using System;
using UnityEngine;

public class DialogInteractArgs : EventArgs
{
    public DialogInteractArgs(DialogData[] dialogDataList, Action OnDialogComplete)
    {
        this.dialogDataList = dialogDataList;
        this.OnDialogComplete = OnDialogComplete;
    }

    public DialogData[] dialogDataList;
    public Action OnDialogComplete;
}
