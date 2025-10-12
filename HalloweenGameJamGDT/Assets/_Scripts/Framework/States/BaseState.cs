using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected Action OnStateComplete;
    public abstract void StateEnter(Action action);
    public abstract void StateUpdate(float deltaTime);
    public virtual void StateFixedUpdate(float fixedDeltaTime){}
    public abstract void StateExit();
    public override abstract string ToString();
}
