using UnityEngine;
using System;

public abstract class BaseStateMachine : MonoBehaviour
{
    protected BaseState current_state;
    protected BaseState[] available_states;
    protected abstract void SwitchState();
    protected virtual void Awake() => available_states = GetComponents<BaseState>();
}

