using UnityEngine;
using System;
using UnityEngine.Events;

public abstract class BaseStateMachine : MonoBehaviour
{
    public UnityEvent<BaseState> OnStateSwitch;
    protected BaseState current_state;
    protected BaseState[] available_states;
    protected virtual void Awake() => available_states = GetComponents<BaseState>();
    
    protected void SwitchState(BaseState newState)
    {
        current_state?.StateExit();
        current_state = newState;
        current_state?.StateEnter(OnStateComplete);

        OnStateSwitch?.Invoke(current_state);
    }

    private void Update() => current_state?.StateUpdate(Time.deltaTime);
    private void FixedUpdate() => current_state?.StateFixedUpdate(Time.fixedDeltaTime);

    protected abstract void OnStateComplete();
}

