using UnityEngine;

public abstract class BasePlayerState : BaseState
{
    [SerializeField] protected PlayerStateType playerStateType;
    [SerializeField] protected bool can_be_interrupted = true;

    public bool CanStateBeInterrupted => can_be_interrupted;
    public PlayerStateType GetPlayerStateType => playerStateType;

    public virtual void StateHasBeenInterrupted(){}
}
