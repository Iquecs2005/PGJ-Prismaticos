using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject promptRoot;
    [SerializeField] private RectTransform promptRect;
    [SerializeField] private Camera worldCamera;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1f, 0f);

    private Transform target;

    private void Awake()
    {
        if (promptRoot != null) promptRoot.SetActive(false);
    }
    public void Show(Transform newTarget)
    {
        target = newTarget;
        if (promptRoot != null) promptRoot.SetActive(target != null);
    }
    public void Hide()
    {
        target = null;
        if (promptRoot != null) promptRoot.SetActive(false);
    }
    private void LateUpdate()
    {
        if (target == null) return;

        if (worldCamera == null) worldCamera = Camera.main;
        if (worldCamera == null || promptRect == null) return;

        promptRect.position = worldCamera.WorldToScreenPoint(target.position + worldOffset);
    }
}