using System;
using UnityEngine;

public class InteractState : BasePlayerState
{
    [SerializeField] private InteractableDetector interactTableDetector;
    private Interactable _currentInteractTable;

    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
        _currentInteractTable = interactTableDetector.GetClosetsInteractTable;

        _currentInteractTable.UpdateInteractableUI(false);
        _currentInteractTable.Interact(OnInteractionComplete);
    }

    public override void StateExit()
    {
        _currentInteractTable.UpdateInteractableUI(true);  
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
