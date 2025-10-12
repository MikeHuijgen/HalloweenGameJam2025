using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractTableDetector : MonoBehaviour
{
    private List<IInteractTable> _availableInteractions;
    
    private void Awake() => _availableInteractions = new List<IInteractTable>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IInteractTable>(out var interactTable)) return;
        if (_availableInteractions.Contains(interactTable)) return;
        _availableInteractions.Add(interactTable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<IInteractTable>(out var interactTable)) return;
        if (!_availableInteractions.Contains(interactTable)) return;
        _availableInteractions.Remove(interactTable);
    }

    public IInteractTable[] GetInteractTables => _availableInteractions.ToArray();
    public bool HasInteractTables => _availableInteractions.Count > 0;
}
