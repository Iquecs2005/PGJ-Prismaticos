using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    public void SetMoveInput(Vector2 moveInput)
    {
        animator.SetFloat("MoveInput", moveInput.magnitude);
    }
}
