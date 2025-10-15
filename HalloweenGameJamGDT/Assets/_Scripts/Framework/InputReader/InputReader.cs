using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : GenericSingleton<InputReader>
{
    public EventHandler<PlayerStateType> OnPlayerStateInput;
    public EventHandler OnNextTextInput;
    private PlayerInput _playerInput;
    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
        SwitchToGameplayActionMap();
    }

    private void OnEnable() => SubToEvents();
    private void OnDisable() => UnSubFromEvents();

    private void SubToEvents()
    {
        _playerInput.actions["Move"].performed += OnMoveActionPreformed;
        _playerInput.actions["Interact"].performed += OnInteractActionPreformed;
        _playerInput.actions["Jump"].performed += OnJumpActionPreformed;

        _playerInput.actions["NextText"].performed += OnNextTextPreformed;
    }

    private void UnSubFromEvents()
    {
        _playerInput.actions["Move"].performed -= OnMoveActionPreformed;
        _playerInput.actions["Interact"].performed -= OnInteractActionPreformed;
        _playerInput.actions["Jump"].performed -= OnJumpActionPreformed;

        _playerInput.actions["NextText"].performed -= OnNextTextPreformed;
    }

    private void OnMoveActionPreformed(InputAction.CallbackContext context) => OnPlayerStateInput?.Invoke(this, PlayerStateType.MoveState);
    private void OnInteractActionPreformed(InputAction.CallbackContext context) => OnPlayerStateInput?.Invoke(this, PlayerStateType.InteractState);
    private void OnJumpActionPreformed(InputAction.CallbackContext context) => OnPlayerStateInput?.Invoke(this, PlayerStateType.JumpState);
    private void OnNextTextPreformed(InputAction.CallbackContext context) => OnNextTextInput?.Invoke(this, null);

    public Vector2 GetMoveValue => _playerInput.actions["Move"].ReadValue<Vector2>();
    public Vector2 GetMouseDelta => _playerInput.actions["Camera"].ReadValue<Vector2>();
    public void SwitchToGameplayActionMap() => _playerInput.SwitchCurrentActionMap("Gameplay");
    public void SwitchToDialogActionMap() => _playerInput.SwitchCurrentActionMap("Dialog");
}
