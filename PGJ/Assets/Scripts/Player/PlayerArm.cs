using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInput;

    private void Update()
    {
        RotateArm();
    }

    public void RotateArm() 
    {
        Vector2 aimPosition = playerInput.MouseWorldPosition();
        Vector2 dir = (aimPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
