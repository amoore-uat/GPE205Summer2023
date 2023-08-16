using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BackToPost };

    public float attackRange = 100f;
    public AIState currentState = AIState.Scan;
    private float lastStateChangeTime = 0f;
    public GameObject target;
    public Transform post;
    public float fieldOfView = 30f;
    public Waypoint currentWaypoint;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        post = transform;
        currentWaypoint = GameManager.Instance.GetRandomWaypoint();
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
            case AIState.Idle:
                // Do that state's behavior
                DoIdleState();
                // Check for transitions
                foreach (Controller playerController in GameManager.Instance.players)
                {
                    
                    if (CanSee(playerController.gameObject))
                    {
                        Debug.Log("I saw a player");
                        target = playerController.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.gameObject))
                    {
                        ChangeAIState(AIState.Scan);
                        return;
                    }
                }
                break;
            case AIState.Attack:
                // Do that state's behavior
                DoAttackState();
                // Check for transitions
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) > attackRange)
                {
                    ChangeAIState(AIState.Chase);
                    return;
                }
                if (!CanSee(target))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }
                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();
                // Check for transitions
                if (!CanSee(target))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) <= attackRange)
                {
                    ChangeAIState(AIState.Attack);
                    return;
                }
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
                foreach (Controller playerController in GameManager.Instance.players)
                {
                    if (CanSee(playerController.gameObject))
                    {
                        target = playerController.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                }
                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.BackToPost);
                    return;
                }
                break;
            case AIState.BackToPost:
                // Do that state's behavior
                DoBackToPostState();
                // Check for transitions
                if (Vector3.SqrMagnitude(post.position - transform.position) <= 1f)
                {
                    ChangeAIState(AIState.Idle);
                    return;
                }
                break;
            default:
                Debug.LogWarning("AI Controller doesn't have that state implemented");
                break;
        }
    }

    private bool CanHear(GameObject targetGameObject)
    {
        return false;
    }

    private bool CanSee(GameObject targetGameObject)
    {
        Vector3 agentToTargetVector = targetGameObject.transform.position - transform.position;
        
        if (Vector3.Angle(agentToTargetVector, transform.forward) <= fieldOfView)
        {
            
            Vector3 raycastDirection = targetGameObject.transform.position - pawn.transform.position;
            RaycastHit hit;
            Physics.Raycast(transform.position, raycastDirection, out hit);
            if (Physics.Raycast(transform.position, raycastDirection, out hit))
            {
                if (hit.collider.transform.parent != null)
                {
                    return (hit.collider.transform.parent.gameObject == targetGameObject);
                }
            }
        }
        return false;
    }

    private void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);
        pawn.Shoot();
    }

    private void DoChaseState()
    {
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
        // Rotate Clockwise
        pawn.Rotate(1f);
    }

    private void DoBackToPostState()
    {
        //throw new NotImplementedException();
        pawn.RotateTowards(post.position);
        pawn.MoveForward();
    }

    private void DoIdleState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
