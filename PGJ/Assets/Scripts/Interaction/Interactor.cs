using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;
    [Header("UI")]
    [SerializeField] private UnityEvent<bool> onFocusChanged;

    private readonly List<IInteractable> inRange = new List<IInteractable>();
    private IInteractable focused;
    private IInteractable holding;

    private void OnEnable()
    {
        if (interactAction == null) return;
        interactAction.action.performed += OnInteractPerformed;
        interactAction.action.canceled += OnInteractCanceled;
        interactAction.action.Enable();
    }
    private void OnDisable()
    {
        if (interactAction == null) return;
        interactAction.action.performed -= OnInteractPerformed;
        interactAction.action.canceled -= OnInteractCanceled;
    }
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (focused == null) return;
        holding = focused;
        holding.StartInteract(gameObject);
    }
    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        if (holding == null || (holding as MonoBehaviour) == null)
        {
            holding = null;
            return;
        }
        holding.CancelInteract(gameObject);
        holding = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !inRange.Contains(interactable))
            inRange.Add(interactable);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            inRange.Remove(interactable);
    }
    private void Update()
    {
        IInteractable nearest = GetNearestUsable();
        if (!ReferenceEquals(nearest, focused))
        {
            focused?.OnFocusExit();
            focused = nearest;
            focused?.OnFocusEnter();
            onFocusChanged?.Invoke(focused != null);
        }
    }
    private IInteractable GetNearestUsable()
    {
        IInteractable nearest = null;
        float best = float.MaxValue;

        for (int i = inRange.Count - 1; i >= 0; i--)
        {
            IInteractable candidate = inRange[i];
            if (candidate == null || (candidate as MonoBehaviour) == null)
            {
                inRange.RemoveAt(i);
                continue;
            }
            if (!candidate.CanInteract) continue;
            var mono = candidate as MonoBehaviour;
            float dist = ((Vector2)mono.transform.position - (Vector2)transform.position).sqrMagnitude;
            if (dist < best)
            {
                best = dist;
                nearest = candidate;
            }
        }
        return nearest;
    }
}