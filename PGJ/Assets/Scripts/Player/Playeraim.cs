using UnityEngine;

public class PlayerAim : PlayerHub
{
    [Tooltip("Sprite em world-space que segue o mouse.")]
    [SerializeField] private Transform crosshair;
    [Tooltip("Esconder o cursor do Windows enquanto mira.")]
    [SerializeField] private bool hideSystemCursor = true;

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
        if (!Initialized || crosshair == null) return;

        Vector2 mouse = player.MouseWorldPosition();
        crosshair.position = new Vector3(mouse.x, mouse.y, crosshair.position.z);
    }
}
