using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    [Header("Eventos de UI (prompt)")]
    [SerializeField] private UnityEvent<IInteractable> onInteractableChanged;

    private readonly List<IInteractable> inRange = new List<IInteractable>();
    private IInteractable focused;
    private IInteractable holding;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !inRange.Contains(interactable))
            inRange.Add(interactable);
    }
    private void Update()
    {
        IInteractable nearest = GetNearestUsable();
        if (!ReferenceEquals(nearest, focused))
        {
            focused?.OnFocusExit();
            focused = nearest;
            focused?.OnFocusEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && inRange.Remove(interactable))
        {
            Debug.Log($"[Interactor] '{other.name}' saiu do alcance. Total: {inRange.Count}");
            RefreshPrompt();
        }
    }

    public void OnInteractPressed()
    {
        if (focused == null) return;
        holding = focused;
        holding.StartInteract(gameObject);
    }
    public void OnInteractReleased()
    {
        if (holding == null || (holding as MonoBehaviour) == null)
        {
            holding = null;
            return;
        }
        holding.CancelInteract(gameObject);
        holding = null;
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

    private void RefreshPrompt()
    {
        IInteractable target = GetNearestUsable();
        onInteractableChanged?.Invoke(target);
    }
}