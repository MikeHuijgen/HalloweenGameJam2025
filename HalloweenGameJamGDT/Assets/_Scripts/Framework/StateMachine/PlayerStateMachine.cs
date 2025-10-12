using System;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    private void OnPlayerStateInput(object sender, PlayerStateType newPlayerStateType)
    {
        var newState = GetStateByPlayerStateType(newPlayerStateType);
    }

    protected override void SwitchState()
    {
        current_state?.StateExit();
        //current_state = GetStateByPlayerStateType(StateType);
        current_state?.StateEnter();
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
}
