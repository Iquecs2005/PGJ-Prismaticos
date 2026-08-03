using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFishAI : RandomMovementAI
{
    [SerializeField] private GameObject deadBody;

    private GameObject lastThreat;

    private bool frightened = false;

    public void OnNewObjectOnView() 
    {
        GameObject closestThreat = controller.vision.GetClosestObjectOnView(transform.position);

        if (closestThreat != null) 
        {
            ChangeState(FishState.Fleeing);
            lastThreat = closestThreat;
        }
        else
        {
            frightened = false;
        }
    }

    protected override void OnFleeing()
    {
        base.OnFleeing();

        frightened = true;
    }

    protected override void FleeUpdate()
    {
        Vector2 dir = transform.position - lastThreat.transform.position;
        controller.movement.SetMoveInput(dir);
        
        if (!frightened)
        {
            base.FleeUpdate();
        }
    }

    public void OnDeath() 
    {
        Instantiate(deadBody, transform.position, transform.rotation);
    }
}
