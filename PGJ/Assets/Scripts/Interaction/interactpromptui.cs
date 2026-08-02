using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1f, 0f);

    private Transform target;
    private Interactor interactor;

    private void Start()
    {
        holder?.SetActive(false);

        PlayerController player = GameManager.playerController;
        if (player != null) 
            interactor = player.GetComponentInChildren<Interactor>();

        interactor?.OnFocusChanged.AddListener(HandleFocusChanged);
    }

    private void OnDestroy()
    {
        interactor?.OnFocusChanged.RemoveListener(HandleFocusChanged);
    }

    public void Show(Transform newTarget)
    {
        target = newTarget;
        holder?.SetActive(target != null);
        PositionOnTarget();
    }

    public void Hide()
    {
        target = null;
        holder?.SetActive(false);
    }

    private void FixedUpdate()
    {
        PositionOnTarget();
    }

    private void PositionOnTarget() 
    {
        if (target == null)
            return;

        transform.position = Camera.main.WorldToScreenPoint(target.position + worldOffset);
    }

    private void HandleFocusChanged(Transform target)
    {
        if (target != null) 
            Show(target);
        else 
            Hide();
    }
}