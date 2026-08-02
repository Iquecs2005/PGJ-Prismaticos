using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFishAI : MonoBehaviour
{
    [SerializeField] private BasicFishController controller;

    [SerializeField] private float fleeTime;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    [SerializeField] private float minMovingTime;
    [SerializeField] private float maxMovingTime;

    private GameObject lastThreat;

    private bool frightened = false;

    private enum FishState 
    {
        Idle, Moving, Fleeing
    }
    private FishState currentState;

    private float fleeingTimer;
    private float idleTimer;
    private float movingTimer;

    private void Start()
    {
        ChangeState(FishState.Idle);
    }

    private void FixedUpdate()
    {
        switch (currentState) 
        {
            case FishState.Idle:
                Idle();
                break;
            case FishState.Moving:
                Move();
                break;
            case FishState.Fleeing:
                Flee();
                break;
        }
    }

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
            fleeingTimer = fleeTime;
        }
    }

    private void ChangeState(FishState newState) 
    {
        currentState = newState;
        switch (newState)
        {
            case FishState.Idle:
                controller.movement.SetMoveInput(Vector2.zero);
                idleTimer = Random.Range(minIdleTime, maxIdleTime);
                break;
            case FishState.Moving:
                Vector2 randomDir = Random.insideUnitCircle;
                controller.movement.SetMoveInput(randomDir);
                movingTimer = Random.Range(minMovingTime, maxMovingTime);
                break;
            case FishState.Fleeing:
                frightened = true;
                break;
        }
    }

    private void Flee() 
    {
        Vector2 dir = transform.position - lastThreat.transform.position;
        controller.movement.SetMoveInput(dir);

        if (!frightened)
        {
            fleeingTimer -= Time.deltaTime;
            if (fleeingTimer <= 0)
            {
                ChangeState(FishState.Idle);
            }
        }
    }

    private void Idle() 
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            ChangeState(FishState.Moving);
        }
    }

    private void Move() 
    {
        movingTimer -= Time.deltaTime;
        if (movingTimer <= 0)
        {
            ChangeState(FishState.Idle);
        }
    }
}
