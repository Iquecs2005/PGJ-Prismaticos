using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [field: Header("Player References")]
    [field: SerializeField] public Collider2D col { get; private set; }
    [field: SerializeField] public SpriteRenderer bodySpriteRenderer { get; private set; }
    [field: SerializeField] public SpriteRenderer armSpriteRenderer { get; private set; }

    [field: SerializeField] public HealthController health { get; private set; }
    [field: SerializeField] public PlayerStamina stamina { get; private set; }
    [field: SerializeField] public PlayerHunger hunger { get; private set; }
    [field: SerializeField] public DynamicInventory inventory { get; private set; }
    [field: SerializeField] public PlayerInputController input { get; private set; }

    public bool IsFacingRight { get; private set; } = true;

    void Start()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (bodySpriteRenderer == null) bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        GameManager.cameraController?.SetFollowTarget(transform);
    }
}