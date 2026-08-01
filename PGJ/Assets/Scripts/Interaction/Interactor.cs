using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    [Header("Eventos de UI (prompt)")]
    [SerializeField] private UnityEvent<bool, string> onInteractableChanged;

    private readonly List<IInteractable> inRange = new List<IInteractable>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[Interactor] Trigger ENTER com '{other.name}'", other);

        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !inRange.Contains(interactable))
        {
            inRange.Add(interactable);
            Debug.Log($"[Interactor] '{other.name}' e IInteractable -> adicionado. Total no alcance: {inRange.Count}");
            RefreshPrompt();
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
        Debug.Log($"[Interactor] OnInteractPressed! Itens no alcance: {inRange.Count}");

        IInteractable target = GetNearestUsable();
        if (target == null)
        {
            Debug.LogWarning("[Interactor] Nenhum interativo usavel no alcance (target == null).");
            return;
        }

        Debug.Log($"[Interactor] Interagindo com '{(target as MonoBehaviour).name}'");
        target.Interact(gameObject);
        RefreshPrompt();
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
        bool hasTarget = target != null;
        string prompt = hasTarget ? target.InteractPrompt : "";
        onInteractableChanged?.Invoke(hasTarget, prompt);
    }
}