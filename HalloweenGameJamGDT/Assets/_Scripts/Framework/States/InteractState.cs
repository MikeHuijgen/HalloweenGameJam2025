using System;
using UnityEngine;

public class InteractState : BasePlayerState
{
    [SerializeField] private InteractTableDetector interactTableDetector;
    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
        var interactTables = interactTableDetector.GetInteractTables;

        foreach (var interactTable in interactTables)
        {
            interactTable.Interact(OnInteractionComplete);
        }
    }

    public override void StateExit()
    {
        
    }

    public override void StateHasBeenInterrupted()
    {
        
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
