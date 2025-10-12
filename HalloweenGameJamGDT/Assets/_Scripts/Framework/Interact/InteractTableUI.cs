using UnityEngine;

public class InteractTableUI : MonoBehaviour
{
    [SerializeField] private InteractTable interactTable;
    [SerializeField] private GameObject InteractTableUIGameObject;

    private void Awake() => InteractTableUIGameObject.SetActive(false);
    private void OnEnable() => interactTable.OnUpdateInteractTableUI += UpdateUI;
    private void OnDisable() => interactTable.OnUpdateInteractTableUI -= UpdateUI;

    private void UpdateUI(object sender, bool value)
    {
        InteractTableUIGameObject.SetActive(value);
    }
}
