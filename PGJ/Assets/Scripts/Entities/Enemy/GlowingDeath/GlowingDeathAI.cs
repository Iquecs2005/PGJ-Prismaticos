using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingDeathAI : BaseAI
{
    private GameObject prey;

    protected override void OnChasing()
    {
        prey = GameManager.playerController.gameObject;
    }

    protected override void ChaseUpdate()
    {
        Vector2 dir = prey.transform.position - transform.position;
        controller.movement.SetMoveInput(dir);
    }

    public void OnDeath() 
    {
        print("You win");
        if (WinManager.Instance != null)
            WinManager.Instance.Win();
    }
}
