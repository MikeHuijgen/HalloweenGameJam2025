using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void StateExit();
}
