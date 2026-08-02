using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    [Header("Base Interactable Variables")]
    [SerializeField] protected float holdDuration = 0.8f;
    [SerializeField] protected bool singleUse;
    [SerializeField] protected float reuseCooldown;

    [Header("Base Interactable Events")]
    [SerializeField] protected UnityEvent onInteractStarted;
    [SerializeField] protected UnityEvent onInteractCompleted;
    [SerializeField] protected UnityEvent onInteractCanceled;

    protected bool isInteracting;

    private Coroutine holdCoroutine;
    private PlayerController player;
    private float currentCooldown;

    public bool CanInteract => _CanInteract();
    public bool OnCooldown => currentCooldown > 0;

    protected virtual void Update()
    {
        if (OnCooldown)
            currentCooldown -= currentCooldown;
    }

    public virtual void StartInteract(GameObject interactor)
    {
        if (!CanInteract) 
            return;

        isInteracting = true;

        player = interactor.GetComponentInParent<PlayerController>();
        player.SetMovementLocked(true);

        onInteractStarted?.Invoke();
        holdCoroutine = StartCoroutine(HoldRoutine(interactor));
    }

    public virtual void CancelInteract(GameObject interactor)
    {
        if (!isInteracting) 
            return;

        if (holdCoroutine != null) 
            StopCoroutine(holdCoroutine);

        isInteracting = false;
        holdCoroutine = null;
        UnlockMovement();

        onInteractCanceled?.Invoke();
    }

    protected virtual IEnumerator HoldRoutine(GameObject interactor)
    {
        yield return new WaitForSeconds(holdDuration);
        InteractionComplete();
    }

    protected virtual void InteractionComplete() 
    {
        isInteracting = false;
        holdCoroutine = null;
        currentCooldown = reuseCooldown;

        UnlockMovement();

        onInteractCompleted?.Invoke();

        if (singleUse) 
            Destroy(gameObject);
    }

    protected virtual bool _CanInteract() 
    {
        if (isInteracting)
            return false;
        if (OnCooldown)
            return false;

        return true;
    }
    
    private void UnlockMovement() 
    {
        player.SetMovementLocked(false);
        player = null;
    }
}
