using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementAI : BaseAI
{
    protected override void OnIdle()
    {
        base.OnIdle();

        controller.movement.SetMoveInput(Vector2.zero);
    }

    protected override void OnMoving()
    {
        base.OnMoving();

        Vector2 randomDir = Random.insideUnitCircle;
        controller.movement.SetMoveInput(randomDir);
    }
}
