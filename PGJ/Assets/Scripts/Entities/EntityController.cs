using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [field: Header("Entity References")]
    [field: SerializeField] public Rigidbody2D rb { get; protected set; }
    [field: SerializeField] public EntityMovement movement { get; protected set; }
    [field: SerializeField] public GameObject mainSpriteObject { get; private set; }

    public bool movementLocked { get; protected set; }

    private void Start()
    {
        if (rb == null) 
            rb = GetComponent<Rigidbody2D>();
    }

    public void SetMovementLocked(bool locked) 
    {
        movementLocked = locked;
    } 
}
