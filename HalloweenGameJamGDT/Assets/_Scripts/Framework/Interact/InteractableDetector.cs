using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    private List<Interactable> _availableInteractions;
    private Interactable _closestInteractable;
    
    private void Awake() => _availableInteractions = new List<Interactable>();

    private void Update()
    {
        CalculateClosestInteractable();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Interactable>(out var interactable)) return;
        if (_availableInteractions.Contains(interactable)) return;
        _availableInteractions.Add(interactable);

        if (_closestInteractable != null && _availableInteractions.Count > 1) return;
        _closestInteractable = interactable;
        _closestInteractable.UpdateInteractableUI(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Interactable>(out var interactable)) return;
        if (!_availableInteractions.Contains(interactable)) return;
        _availableInteractions.Remove(interactable);
        interactable.UpdateInteractableUI(false);

        if (_availableInteractions.Count == 0) _closestInteractable = null;
    }

    private void CalculateClosestInteractable()
    {
        if (_availableInteractions.Count <= 1) return;

        foreach (var interactable in _availableInteractions)
        {
            var newDistance = Vector3.Distance(transform.position, interactable.transform.position);
            if (newDistance >= Vector3.Distance(transform.position, _closestInteractable.transform.position)) continue;

            _closestInteractable.UpdateInteractableUI(false);
            _closestInteractable = interactable;
            _closestInteractable.UpdateInteractableUI(true);
        }
    }

    public Interactable GetClosetsInteractTable => _closestInteractable;
    public bool HasInteractables => _availableInteractions.Count > 0;
}
