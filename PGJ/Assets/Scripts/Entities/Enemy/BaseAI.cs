using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    [Header("Basic AI References")]
    [SerializeField] protected EnemyController controller;

    [Header("Basic AI States")]
    [SerializeField] protected FishState initialState;
    [SerializeField] protected FishState onIdleEndState;
    [SerializeField] protected FishState onMovingEndState;
    [SerializeField] protected FishState onFleeingEndState;
    [SerializeField] protected FishState onChasingEndState;

    [Header("State Timers")]
    [SerializeField] protected float minIdleTime;
    [SerializeField] protected float maxIdleTime;
    [SerializeField] protected float minMovingTime;
    [SerializeField] protected float maxMovingTime;
    [SerializeField] protected float minFleeingTime;
    [SerializeField] protected float maxFleeingTime;
    [SerializeField] protected float minChasingTime;
    [SerializeField] protected float maxChasingTime;

    protected float idleTimer;
    protected float movingTimer;
    protected float fleeingTimer;
    protected float chasingTimer;

    protected FishState currentState;

    private void Start()
    {
        ChangeState(initialState);
    }

    protected virtual void FixedUpdate() 
    {
        switch (currentState)
        {
            case FishState.Idle:
                IdleUpdate();
                break;
            case FishState.Moving:
                MoveUpdate();
                break;
            case FishState.Fleeing:
                FleeUpdate();
                break;
            case FishState.Chasing:
                ChaseUpdate();
                break;
        }
    }

    protected virtual void ChangeState(FishState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case FishState.Idle:
                OnIdle();
                break;
            case FishState.Moving:
                OnMoving();
                break;
            case FishState.Fleeing:
                OnFleeing();
                break;
            case FishState.Chasing:
                OnChasing();
                break;
        }
    }

    protected virtual void OnIdle() 
    {
        idleTimer = Random.Range(minIdleTime, maxIdleTime);
    }

    protected virtual void OnMoving()
    {
        movingTimer = Random.Range(minMovingTime, maxMovingTime);
    }

    protected virtual void OnFleeing()
    {
        fleeingTimer = Random.Range(minFleeingTime, maxFleeingTime);
    }

    protected virtual void OnChasing()
    {
        chasingTimer = Random.Range(minChasingTime, maxChasingTime);
    }

    protected virtual void IdleUpdate() 
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            ChangeState(onIdleEndState);
        }
    }

    protected virtual void MoveUpdate()
    {
        movingTimer -= Time.deltaTime;
        if (movingTimer <= 0)
        {
            ChangeState(onMovingEndState);
        }
    }

    protected virtual void FleeUpdate()
    {
        fleeingTimer -= Time.deltaTime;
        if (fleeingTimer <= 0)
        {
            ChangeState(onFleeingEndState);
        }
    }

    protected virtual void ChaseUpdate()
    {
        chasingTimer -= Time.deltaTime;
        if (chasingTimer <= 0)
        {
            ChangeState(onChasingEndState);
        }
    }
}

public enum FishState
{
    Idle, Moving, Fleeing, Chasing
}