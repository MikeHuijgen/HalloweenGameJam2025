using System;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    public static EventHandler<BaseState> OnSwitchPlayerState;
    [SerializeField] private BaseState defaultState;

    protected override void Awake()
    {
        base.Awake();
        SwitchState(defaultState);
        OnSwitchPlayerState?.Invoke(this, current_state);
    }

    private void OnPlayerStateInput(object sender, PlayerStateType newPlayerStateType)
    {
        var currentPlayerState = (BasePlayerState)current_state;
        if (currentPlayerState.GetPlayerStateType == newPlayerStateType) return;
        if (!currentPlayerState.CanStateBeInterrupted) return;

        var newState = GetStateByPlayerStateType(newPlayerStateType);
        if (newState == null) return;

        SwitchState(newState);
        OnSwitchPlayerState?.Invoke(this, current_state);
    }

    private BaseState GetStateByPlayerStateType(PlayerStateType playerStateType)
    {
        foreach (var State in available_states)
        {
            var playerState = (BasePlayerState)State;
            if (playerState.GetPlayerStateType != playerStateType) continue;

            return State;
        }

        return null;
    }

    private void OnEnable() => InputReader.Instance.OnPlayerStateInput += OnPlayerStateInput;
    private void OnDisable() => InputReader.Instance.OnPlayerStateInput -= OnPlayerStateInput;

    protected override void OnStateComplete()
    {
        SwitchState(defaultState);    
        OnSwitchPlayerState?.Invoke(this, current_state);  
    }
}
