using UnityEngine;
using UnityEngine.Events;

public class TriggerBlock : MonoBehaviour
{
    public UnityEvent OnTrigger;
    private BoxCollider _triggerCollider;

    private void Awake() => _triggerCollider = GetComponent<BoxCollider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _triggerCollider.enabled = false;
        OnTrigger?.Invoke();
    }
}
