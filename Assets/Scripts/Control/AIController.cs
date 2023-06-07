using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost };
    public AIState currentState = AIState.Chase;
    private float lastStateChangeTime = 0f;
    public GameObject target;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        base.Start();
    }

    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Guard:
                // Do that state's behavior
                DoGuardState();
                // Check for transitions
                break;
            case AIState.Attack:
                // Do that state's behavior
                DoAttackState();
                // Check for transitions
                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();
                // Check for transitions
                break;
            case AIState.Flee:
                // Do that state's behavior
                DoFleeState();
                // Check for transitions
                break;
            case AIState.Patrol:
                // Do that state's behavior
                DoPatrolState();
                // Check for transitions
                break;
            case AIState.Scan:
                // Do that state's behavior
                DoScanState();
                // Check for transitions
                break;
            case AIState.BackToPost:
                // Do that state's behavior
                DoBackToPostState();
                // Check for transitions
                break;
            default:
                Debug.LogWarning("AI Controller doesn't have that state implemented");
                break;
        }
    }

    private void DoAttackState()
    {
        //throw new NotImplementedException();
    }

    private void DoChaseState()
    {
        //throw new NotImplementedException();
        // Turn to face target
        pawn.RotateTowards(target.transform.position);
        // Move forward
        pawn.MoveForward();
    }

    private void DoFleeState()
    {
        //throw new NotImplementedException();
    }

    private void DoPatrolState()
    {
        //throw new NotImplementedException();
    }

    private void DoScanState()
    {
        //throw new NotImplementedException();
    }

    private void DoBackToPostState()
    {
        //throw new NotImplementedException();
    }

    private void DoGuardState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
