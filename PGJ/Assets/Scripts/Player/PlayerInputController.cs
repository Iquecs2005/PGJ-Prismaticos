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

    public void MoveAction(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        onMoveAction.Invoke(moveInput);
    }

    public void KnifeAction(InputAction.CallbackContext context)
    {
        if (context.performed)
            onKnifeAction.Invoke(MouseWorldPosition());
    }

    public void HarpoonAction(InputAction.CallbackContext context)
    {
        if (context.started)
            onHarpoonAction.Invoke(MouseWorldPosition());
    }

    public void InteractAction(InputAction.CallbackContext context)
    {
        Debug.Log($"[Input] InteractAction phase={context.phase}");

        if (context.performed)
        {
            Debug.Log("[Input] Interact PERFORMED -> invocando onInteractAction");
            onInteractAction.Invoke();
        }
    }

    public Vector2 MouseWorldPosition()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}