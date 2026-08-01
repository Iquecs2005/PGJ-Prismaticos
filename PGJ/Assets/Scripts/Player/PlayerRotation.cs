using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float minSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float flipAngle;

    [SerializeField] private UnityEvent OnFlipEvent;

    private Rigidbody2D rb;
    private bool flipped = false;

    private void Start()
    {
        rb = controller.rb;
    }

    private void FixedUpdate()
    {
        TurnToSpeed();
    }

    private void TurnToSpeed() 
    {
        Vector2 playerVelocity = rb.velocity;

        if (playerVelocity.magnitude < minSpeed)
            return;

        Vector2 dir = rb.velocity.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        CheckFlip();
    }

    private void CheckFlip() 
    {
        float absAngle = Mathf.Abs(transform.rotation.eulerAngles.z);
        if (flipped) 
        {
            if (absAngle < 90 - flipAngle || absAngle > 270 + flipAngle)
            {
                Flip();
            }
        }
        else 
        {
            if (absAngle > 90 + flipAngle && absAngle < 270 - flipAngle)
            {
                Flip();
            }
        }
    }

    private void Flip() 
    {
        flipped = !flipped;

        GameObject bodyObject = controller.bodyGameObject;
        Transform bodyTransform = bodyObject.transform;
        Vector3 currentScale = bodyTransform.localScale;
        Vector3 newScale = new Vector3(currentScale.x, -currentScale.y, currentScale.z);
        bodyTransform.localScale = newScale; 
    }
}
