using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineConfiner2D confiner;

    private Collider2D originalConfiner;
    private float originalZoom;

    private void Start()
    {
        originalZoom = virtualCamera.m_Lens.OrthographicSize;
        originalConfiner = confiner.m_BoundingShape2D;
    }

    public void SetZoom(float zoom) 
    {
        virtualCamera.m_Lens.OrthographicSize = zoom;
    }

    public void ResetZoom() 
    {
        virtualCamera.m_Lens.OrthographicSize = originalZoom;
    }

    public void SetConfiner(Collider2D newConfiner) 
    {
        confiner.m_BoundingShape2D = newConfiner;
        confiner.InvalidateCache();
    }

    public void ResetConfiner() 
    {
        confiner.m_BoundingShape2D = originalConfiner;
        confiner.InvalidateCache();
    }

    public void SetFollowTarget(Transform target) 
    {
        virtualCamera.Follow = target;
    }
}
