using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [field: Header("Player References")]
    [field: SerializeField] public Collider2D col { get; private set; }
    [field: SerializeField] public SpriteRenderer bodySpriteRenderer { get; private set; }
    [field: SerializeField] public SpriteRenderer armSpriteRenderer { get; private set; }

    public bool IsFacingRight { get; private set; } = true;

    void Start()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (bodySpriteRenderer == null) bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        GameManager.cameraController?.SetFollowTarget(transform);
    }
}