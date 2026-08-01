using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private Interactor interactor;
    [SerializeField] private GameObject promptRoot;
    [SerializeField] private RectTransform promptRect;
    [SerializeField] private Camera worldCamera;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1f, 0f);

    private Transform current;

    private void Awake()
    {
        if (promptRoot != null) promptRoot.SetActive(false);
    }
    private void LateUpdate()
    {
        if (interactor == null) return;

        Transform target = interactor.FocusedTarget;

        if (target != current)
        {
            current = target;
            if (promptRoot != null) promptRoot.SetActive(target != null);
        }
        if (target == null) return;

        if (worldCamera == null) worldCamera = Camera.main;
        if (worldCamera == null || promptRect == null) return;

        promptRect.position = worldCamera.WorldToScreenPoint(target.position + worldOffset);
    }
}