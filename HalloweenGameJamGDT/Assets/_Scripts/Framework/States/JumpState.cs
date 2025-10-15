using System;
using System.Collections;
using UnityEngine;

public class JumpState : BasePlayerState
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float airMoveSpeed;
    [SerializeField] private float fallMultiplier = 2.5f;

    [SerializeField] private GroundChecker groundChecker;
    private Rigidbody _rigidBody;
    private bool _hasLeftTheGround;
    private bool _hasInput;

    private Transform _mainCamera;


    private Vector2 _airMoveDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main.transform;
    }

    public override void StateEnter(Action action)
    {
        OnStateComplete = action;
        _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public override void StateUpdate(float deltaTime)
    {
        _airMoveDirection = InputReader.Instance.GetMoveValue;
        _hasInput = _airMoveDirection.sqrMagnitude > 0.01f;


        if (!groundChecker.IsPlayerGrounded) _hasLeftTheGround = true;

        if(_hasLeftTheGround && groundChecker.IsPlayerGrounded)
        {
            OnStateComplete();
        }
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        var currentVelocity = _rigidBody.linearVelocity;

        if (currentVelocity.y < 0)
        {
            currentVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * fixedDeltaTime;
        }

        if (_hasInput)
        {
            Vector3 forward = _mainCamera.forward; forward.y = 0f; 
            Vector3 right = _mainCamera.right; right.y = 0f; 
            
            forward.Normalize();
            right.Normalize();

            Vector3 moveDir = forward * _airMoveDirection.y + right * _airMoveDirection.x;
            moveDir *= airMoveSpeed;

            _rigidBody.linearVelocity = new Vector3(moveDir.x, currentVelocity.y, moveDir.z);

            if (moveDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * fixedDeltaTime);
            }
        }
        else
        {
            _rigidBody.linearVelocity = new Vector3(0, currentVelocity.y, 0);
        }
    }

    public override void StateExit()
    {
        _hasLeftTheGround = false;
        _rigidBody.linearVelocity = Vector3.zero;
    }

    public override string ToString()
    {
        return "Jump state";
    }
}
