using System;
using UnityEngine;

public class InteractState : BasePlayerState
{
    [SerializeField] private InteractTableDetector interactTableDetector;
    private InteractTable _currentInteractTable;

    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
        _currentInteractTable = interactTableDetector.GetClosetsInteractTable;

        _currentInteractTable.UpdateInteractTableUI(false);
        _currentInteractTable.Interact(OnInteractionComplete);
    }

    public override void StateExit()
    {
        _currentInteractTable.UpdateInteractTableUI(true);  
    }

    private void OnInteractionComplete()
    {
        OnStateComplete();
    }

    public override string ToString()
    {
        return "Interact state";
    }
}
