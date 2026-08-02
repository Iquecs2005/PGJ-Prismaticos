using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private GameObject houseInterior;
    [SerializeField] private GameObject houseExterior;
    [SerializeField] private float insideZoom;
    [SerializeField] private PolygonCollider2D cameraConfiner;

    public void OnHouseEnter() 
    {
        houseInterior.SetActive(true);
        houseExterior.SetActive(false);

        GameManager.cameraController.SetZoom(insideZoom);
        GameManager.cameraController.SetConfiner(cameraConfiner);
    }
    
    public void OnHouseExit() 
    {
        houseInterior.SetActive(false);
        houseExterior.SetActive(true);

        GameManager.cameraController.ResetZoom();
        GameManager.cameraController.ResetConfiner();
    }
}
