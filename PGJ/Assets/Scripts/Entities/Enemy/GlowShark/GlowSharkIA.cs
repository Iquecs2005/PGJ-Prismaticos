using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowSharkIA : RandomMovementAI
{
    [Header("GlowShark Variables")]
    [SerializeField] private float minHurtTime;
    [SerializeField] private float maxHurtTime;

    private GameObject prey;

    private bool targetInSight;
    
    public void OnNewObjectOnView()
    {
        GameObject closestPrey = controller.vision.GetClosestObjectOnView(transform.position);

        if (closestPrey != null)
        {
            Chase(closestPrey);
            targetInSight = true;
        }
        else
        {
            targetInSight = false;
        }
    }

    public void OnHit(HitInformation hit) 
    {
        if (hit.damageDealer != null) 
        {
            Chase(hit.damageDealer);
            chasingTimer = Random.Range(minHurtTime, maxHurtTime);
        }
    }

    private void Chase(GameObject prey) 
    {
        ChangeState(FishState.Chasing);
        this.prey = prey;
    }

    protected override void OnChasing()
    {
        base.OnChasing();
    }

    protected override void ChaseUpdate()
    {
        Vector2 dir = prey.transform.position - transform.position;
        controller.movement.SetMoveInput(dir);

        if (!targetInSight)
        {
            base.ChaseUpdate();
        }
    }
}
