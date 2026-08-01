using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [Header("Nadada")]
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float waterDrag = 4f;
    [SerializeField] private float idleDrag = 6f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = controller.rb;
        rb.drag = waterDrag;
    }

    void FixedUpdate()
    {
        Vector2 input = controller.MovementLocked ? Vector2.zero : moveInput;

        bool swimming = input.sqrMagnitude > 0.01f;
        rb.drag = swimming ? waterDrag : idleDrag;

        if (swimming)
        {
            rb.AddForce(input.normalized * acceleration, ForceMode2D.Force);
            if (rb.velocity.magnitude > maxSpeed)
                rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        if (!controller.MovementLocked && Mathf.Abs(moveInput.x) > 0.01f)
            controller.FaceTowards(moveInput.x);
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        print(moveInput);
    }

}