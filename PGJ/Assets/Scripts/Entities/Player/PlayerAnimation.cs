using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetMoveInput(Vector2 moveInput) 
    {
        animator.SetFloat("MoveInput", moveInput.magnitude);
    }
}
