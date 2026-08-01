using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [field: Header("Referencencias")]
    [field: SerializeField] public Rigidbody2D rb { get; private set; }
    [field: SerializeField] public Collider2D col { get; private set; }
    [field: SerializeField] public GameObject bodyGameObject { get; private set; }
    [field: SerializeField] public SpriteRenderer bodySpriteRenderer { get; private set; }
    [field: SerializeField] public SpriteRenderer armSpriteRenderer { get; private set; }

    public bool MovementLocked { get; private set; }
    public bool IsFacingRight { get; private set; } = true;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<Collider2D>();
        if (bodySpriteRenderer == null) bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetMovementLocked(bool locked) => MovementLocked = locked;
}