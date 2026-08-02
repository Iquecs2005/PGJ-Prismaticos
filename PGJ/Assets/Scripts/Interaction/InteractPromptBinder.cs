using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractPromptBinder : MonoBehaviour
{
    [SerializeField] private InteractPromptUI promptUI;

    private Interactor interactor;

    private void Start()
    {
        PlayerController player = GameManager.playerController;
        if (player != null) interactor = player.GetComponentInChildren<Interactor>();

        if (interactor != null) interactor.OnFocusChanged += HandleFocusChanged;
    }
    private void OnDestroy()
    {
        if (interactor != null) interactor.OnFocusChanged -= HandleFocusChanged;
    }

    private void HandleFocusChanged(Transform target)
    {
        if (promptUI == null) return;

        if (target != null) promptUI.Show(target);
        else promptUI.Hide();
    }
}