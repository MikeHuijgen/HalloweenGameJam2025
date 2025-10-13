using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public EventHandler<bool> OnUpdateInteractableUI;

    protected Action OnInteractComplete;

    public abstract void Interact(Action action);

    public void UpdateInteractableUI(bool value) => OnUpdateInteractableUI?.Invoke(this, value);
}
