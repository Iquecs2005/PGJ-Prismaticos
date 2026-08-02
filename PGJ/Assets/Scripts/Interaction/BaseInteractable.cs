using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BaseInteractable : MonoBehaviour
{
    //public void StartInteract(GameObject interactor)
    //{
    //    if (!CanInteract) return;
    //    routine = StartCoroutine(HoldRoutine(interactor));
    //}
    //public void CancelInteract(GameObject interactor)
    //{
    //    if (!isBusy) return;
    //    if (routine != null) StopCoroutine(routine);
    //    routine = null;
    //    Unlock();
    //    SetFill(0f);
    //    isBusy = false;
    //    onInteractCancel?.Invoke();
    //}
    //private IEnumerator HoldRoutine(GameObject interactor)
    //{
    //    isBusy = true;
    //    onInteractStart?.Invoke();
    //    lockedPlayer = interactor != null ? interactor.GetComponentInParent<PlayerController>() : null;
    //    if (lockedPlayer != null) lockedPlayer.SetMovementLocked(true);

    //    float t = 0f;
    //    while (t < holdDuration)
    //    {
    //        t += Time.deltaTime;
    //        SetFill(t / holdDuration);
    //        yield return null;
    //    }

    //    SpawnItems();
    //    Unlock();
    //    SetFill(0f);

    //    used = true;
    //    cooldownTimer = reuseCooldown;
    //    isBusy = false;
    //    routine = null;
    //    onInteractComplete?.Invoke();

    //    if (icon != null) icon.SetActive(false);
    //    if (singleUse) Destroy(gameObject);
    //}
}
