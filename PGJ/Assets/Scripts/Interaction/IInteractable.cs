using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool CanInteract { get; }
    string InteractPrompt { get; }

    void Interact(GameObject interactor);
}