using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractTable : MonoBehaviour
{
    public EventHandler<bool> OnUpdateInteractTableUI;

    protected Action OnInteractComplete;

    public abstract void Interact(Action action);

    public void UpdateInteractTableUI(bool value) => OnUpdateInteractTableUI?.Invoke(this, value);
}
