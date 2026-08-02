using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityMovement : MonoBehaviour
{
    [Header("Entity References")]
    [SerializeField] private EntityController controller;

    [Header("Basic Movement Variables")]
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float waterDrag = 4f;
    [SerializeField] private float idleDrag = 6f;

    [field: Header("Basic Movement Events")]
    [field: SerializeField] public UnityEvent<Vector2> onMoveInputChange { get; protected set; }

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = controller.rb;
        rb.drag = waterDrag;
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
        onMoveInputChange.Invoke(moveInput);
    }

    public void ApplyMovement()
    {
        Vector2 input = controller.movementLocked ? Vector2.zero : moveInput;

        bool swimming = input.sqrMagnitude > 0.01f;
        rb.drag = swimming ? waterDrag : idleDrag;

        if (swimming)
        {
            rb.AddForce(input.normalized * acceleration, ForceMode2D.Force);
            if (rb.velocity.magnitude > maxSpeed)
                rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
