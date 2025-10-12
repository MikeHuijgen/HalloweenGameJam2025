using System.Collections.Generic;
using UnityEngine;

public class InteractTableDetector : MonoBehaviour
{
    private List<InteractTable> _availableInteractions;
    private InteractTable _closestInteractTable;
    
    private void Awake() => _availableInteractions = new List<InteractTable>();

    private void Update()
    {
        CalculateClosestInteractTable();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<InteractTable>(out var interactTable)) return;
        if (_availableInteractions.Contains(interactTable)) return;
        _availableInteractions.Add(interactTable);

        if (_closestInteractTable != null && _availableInteractions.Count > 1) return;
        _closestInteractTable = interactTable;
        _closestInteractTable.UpdateInteractTableUI(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<InteractTable>(out var interactTable)) return;
        if (!_availableInteractions.Contains(interactTable)) return;
        _availableInteractions.Remove(interactTable);
        interactTable.UpdateInteractTableUI(false);

        if (_availableInteractions.Count == 0) _closestInteractTable = null;
    }

    private void CalculateClosestInteractTable()
    {
        if (_availableInteractions.Count <= 1) return;

        foreach (var interactTable in _availableInteractions)
        {
            var newDistance = Vector3.Distance(transform.position, interactTable.transform.position);
            if (newDistance >= Vector3.Distance(transform.position, _closestInteractTable.transform.position)) continue;

            _closestInteractTable.UpdateInteractTableUI(false);
            _closestInteractTable = interactTable;
            _closestInteractTable.UpdateInteractTableUI(true);
        }
    }

    public InteractTable GetClosetsInteractTable => _closestInteractTable;
    public bool HasInteractTables => _availableInteractions.Count > 0;
}
