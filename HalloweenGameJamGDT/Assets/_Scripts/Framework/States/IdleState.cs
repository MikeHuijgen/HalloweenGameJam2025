using System;
using UnityEngine;

public class IdleState : BasePlayerState
{
    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
    }

    public override void StateExit()
    {

    }

    public override void StateHasBeenInterrupted()
    {

    }

    public override void StateUpdate(float deltaTime)
    {

    }

    public override string ToString() => "Idle State";
}
