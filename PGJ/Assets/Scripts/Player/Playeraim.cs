using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

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
        if (crosshair == null) return;

        Vector2 mouse = controller.MouseWorldPosition();
        crosshair.position = new Vector3(mouse.x, mouse.y, crosshair.position.z);
    }
}
