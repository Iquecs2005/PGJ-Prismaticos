using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    bool CanInteract { get; }
    void StartInteract(GameObject interactor);
    void CancelInteract(GameObject interactor);
}