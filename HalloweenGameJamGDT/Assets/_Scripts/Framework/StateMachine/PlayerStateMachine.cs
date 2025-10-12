using System;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    private void OnPlayerStateInput(object sender, PlayerStateType newPlayerStateType)
    {
        var newState = GetStateByPlayerStateType(newPlayerStateType);

        if (newState == null) return;
        SwitchState(newState);
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
        var newState = GetStateByPlayerStateType(PlayerStateType.IdleState);

        if (newState == null) return;
        SwitchState(newState);      
    }
}
