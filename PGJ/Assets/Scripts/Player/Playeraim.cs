using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAim : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Transform crosshair;
    [SerializeField] private bool hideSystemCursor = true;

    void Awake()
    {
        if (controller == null) controller = GetComponent<PlayerController>();
    }

    void OnEnable()
    {
        if (hideSystemCursor) Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (crosshair == null || controller == null) return;

        Vector2 mouse = controller.MouseWorldPosition();
        crosshair.position = new Vector3(mouse.x, mouse.y, crosshair.position.z);
    }
}