using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : GenericSingleton<InputReader>
{
    public EventHandler<PlayerStateType> OnPlayerStateInput;
    private PlayerInput _playerInput;
    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() => SubToEvents();
    private void OnDisable() => UnSubFromEvents();

    private void SubToEvents()
    {
        _playerInput.actions["Move"].performed += OnMoveActionPreformed;
        _playerInput.actions["Interact"].performed += OnInteractActionPreformed;
    }

    private void UnSubFromEvents()
    {
        _playerInput.actions["Move"].performed -= OnMoveActionPreformed;
        _playerInput.actions["Interact"].performed -= OnInteractActionPreformed;
    }

    private void OnMoveActionPreformed(InputAction.CallbackContext context) => OnPlayerStateInput?.Invoke(this, PlayerStateType.MoveState);
    private void OnInteractActionPreformed(InputAction.CallbackContext context) => OnPlayerStateInput?.Invoke(this, PlayerStateType.InteractState);

    public Vector2 GetMoveValue => _playerInput.actions["Move"].ReadValue<Vector2>();
}
