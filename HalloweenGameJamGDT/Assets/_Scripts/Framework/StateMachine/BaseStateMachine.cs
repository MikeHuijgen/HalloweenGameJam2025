using UnityEngine;
using System;
using UnityEngine.Events;

public abstract class BaseStateMachine : MonoBehaviour
{
    protected BaseState current_state;
    protected BaseState[] available_states;
    protected virtual void Awake() => available_states = GetComponents<BaseState>();
    
    protected void SwitchState(BaseState newState)
    {
        current_state?.StateExit();
        current_state = newState;
        current_state?.StateEnter(OnStateComplete);
    }

    private void Update() => current_state?.StateUpdate(Time.deltaTime);
    private void FixedUpdate() => current_state?.StateFixedUpdate(Time.fixedDeltaTime);

    protected abstract void OnStateComplete();
}

