using System;
using System.Collections;
using UnityEngine;

public class MoveState : BasePlayerState
{
    [SerializeField] private float moveSpeed;

    private Vector2 _moveDirection;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
    }

    public override void StateExit()
    {
        _rigidbody.linearVelocity = Vector3.zero;       
    }

    public override void StateUpdate(float deltaTime)
    {
        _moveDirection = InputReader.Instance.GetMoveValue;

        if (_moveDirection == Vector2.zero)
        {
            OnStateComplete();
        }
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
          _rigidbody.linearVelocity = new Vector3(_moveDirection.x, _rigidbody.linearVelocity.y, _moveDirection.y) * moveSpeed * fixedDeltaTime;      
    }

    public override void StateHasBeenInterrupted()
    {

    }

    public override string ToString() => "Move State";
}
