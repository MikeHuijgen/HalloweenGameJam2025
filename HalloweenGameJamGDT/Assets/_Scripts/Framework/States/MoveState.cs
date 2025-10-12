using System;
using UnityEngine;

public class MoveState : BasePlayerState
{
    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
    }

    public override void StateExit()
    {
        
    }

    public override void StateUpdate(float deltaTime)
    {
        
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        
    }

    public override void StateHasBeenInterrupted()
    {
        
    }

}
