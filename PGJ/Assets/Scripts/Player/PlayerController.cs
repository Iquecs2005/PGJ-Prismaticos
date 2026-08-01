using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [field: Header("Referencencias")]
    [field: SerializeField] public Rigidbody2D rb { get; private set; }
    [field: SerializeField] public Collider2D col { get; private set; }
    [field: SerializeField] public SpriteRenderer spriteRenderer { get; private set; }
    [field: SerializeField] private Camera cam;

    public bool MovementLocked { get; private set; }
    public bool IsFacingRight { get; private set; } = true;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<Collider2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (cam == null) cam = Camera.main;
    }

    public Vector2 MouseWorldPosition()
    {
        if (cam == null || Mouse.current == null) return transform.position;

        Vector3 screen = Mouse.current.position.ReadValue();
        screen.z = Mathf.Abs(cam.transform.position.z);
        return cam.ScreenToWorldPoint(screen);
    }

    public Vector2 AimDirection()
    {
        Vector2 dir = MouseWorldPosition() - (Vector2)transform.position;
        return dir.sqrMagnitude > 0.0001f ? dir.normalized : (IsFacingRight ? Vector2.right : Vector2.left);
    }

    public void SetMovementLocked(bool locked) => MovementLocked = locked;

    public void FaceTowards(float xDirection)
    {
        if (Mathf.Abs(xDirection) < 0.01f) return;

        bool faceRight = xDirection > 0f;
        if (faceRight == IsFacingRight) return;

        IsFacingRight = faceRight;
        spriteRenderer.flipX = !faceRight;
    }
}