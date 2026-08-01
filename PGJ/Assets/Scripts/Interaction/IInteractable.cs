using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool CanInteract { get; }
    void OnFocusEnter();
    void OnFocusExit();
    void StartInteract(GameObject interactor);
    void CancelInteract(GameObject interactor);
}