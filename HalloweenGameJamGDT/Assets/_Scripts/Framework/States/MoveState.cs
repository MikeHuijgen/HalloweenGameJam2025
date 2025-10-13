using System;
using System.Collections;
using UnityEngine;

public class MoveState : BasePlayerState
{
    [SerializeField] private float moveSpeed;

    private Vector2 _moveDirection;
    private Rigidbody _rigidbody;
    private Transform _cameraTransform;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _cameraTransform = Camera.main.transform;
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
        if (_moveDirection == Vector2.zero) return;

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * _moveDirection.y + right * _moveDirection.x).normalized;

        _rigidbody.linearVelocity = move * moveSpeed * fixedDeltaTime;

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * fixedDeltaTime);
        }

    }

    public override void StateHasBeenInterrupted()
    {

    }

    public override string ToString() => "Move State";
}
