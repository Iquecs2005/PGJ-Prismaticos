using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [field: Header("Events")]
    [field: SerializeField] public UnityEvent<Vector2> onMoveAction { get; private set; }
    [field: SerializeField] public UnityEvent<Vector2> onKnifeAction { get; private set; }
    [field: SerializeField] public UnityEvent<Vector2> onHarpoonAction { get; private set; }
    [field: SerializeField] public UnityEvent onInteractAction { get; private set; }
    [field: SerializeField] public UnityEvent onInteractCanceled { get; private set; }
    [field: SerializeField] public UnityEvent<bool> onSprintAction { get; private set; }
    [field: SerializeField] public UnityEvent onPauseAction { get; private set; }
    [field: SerializeField] public UnityEvent onEatAction { get; private set; }

    public void MoveAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.IsPaused) { onMoveAction.Invoke(Vector2.zero); return; }
        Vector2 moveInput = context.ReadValue<Vector2>();
        onMoveAction.Invoke(moveInput);
    }
    public void SprintAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.IsPaused) { onSprintAction.Invoke(false); return; }
        if (context.started)
            onSprintAction.Invoke(true);
        else if (context.canceled)
            onSprintAction.Invoke(false);
    }
    public void PauseAction(InputAction.CallbackContext context)
    {
        if (context.performed)
            onPauseAction.Invoke();
    }

    public void KnifeAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.IsPaused) return;
        if (context.performed)
            onKnifeAction.Invoke(MouseWorldPosition());
    }

    public void HarpoonAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.IsPaused) return;
        if (context.started)
            onHarpoonAction.Invoke(MouseWorldPosition());
    }

    public void InteractAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.IsPaused) return;
        if (context.performed)
            onInteractAction.Invoke();
        else if (context.canceled)
            onInteractCanceled.Invoke();
    }

    public void EatAction(InputAction.CallbackContext context) 
    {
        if (PauseMenu.IsPaused) return;
        if (context.performed)
            onEatAction.Invoke();
    }

    public Vector2 MouseWorldPosition()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}