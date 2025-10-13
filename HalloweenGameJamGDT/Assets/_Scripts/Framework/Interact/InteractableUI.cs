using UnityEngine;

public class InteractTableUI : MonoBehaviour
{
    [SerializeField] private Interactable interactTable;
    [SerializeField] private GameObject InteractableUIGameObject;

    private void Awake() => InteractableUIGameObject.SetActive(false);
    private void OnEnable() => interactTable.OnUpdateInteractableUI += UpdateUI;
    private void OnDisable() => interactTable.OnUpdateInteractableUI -= UpdateUI;

    private void UpdateUI(object sender, bool value)
    {
        InteractableUIGameObject.SetActive(value);
    }
}
