using TMPro;
using UnityEngine;

public class StateDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    void OnEnable() =>  PlayerStateMachine.OnSwitchPlayerState += UpdateDebugText;
    void OnDisable() => PlayerStateMachine.OnSwitchPlayerState -= UpdateDebugText;
    
    public void UpdateDebugText(object sender, BaseState state)
    {
        debugText.text = state.ToString();
    }
}
