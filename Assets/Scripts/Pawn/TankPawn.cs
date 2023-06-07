using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMover))]
[RequireComponent(typeof(TankShooter))]
public class TankPawn : Pawn
{
    private const float ForwardDirection = 1f;
    private const float BackwardDirection = -1f;
    public float forwardMoveSpeed = 5f;
    public float backwardMoveSpeed = 3f;
    public float tankRotationSpeed = 60f;
    public float fireForce = 1000f;
    public float damageDone = 20f;
    public float shellLifespan = 1.5f;
    public GameObject shellPrefab;
    public float shotCooldownTime = 1f;
    private float secondsSinceLastShot = Mathf.Infinity;

    public override void MoveBackward()
    {
        mover.Move(backwardMoveSpeed, BackwardDirection);
        base.MoveBackward();
    }

    public override void MoveForward()
    {
        mover.Move(forwardMoveSpeed, ForwardDirection);
        base.MoveForward();
    }

    public override void Rotate(float direction)
    {
        mover.Rotate(tankRotationSpeed, direction);
        base.Rotate(direction);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<TankMover>();
        shooter = GetComponent<TankShooter>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
        base.Update();
    }

    public override void Shoot()
    {
        if (secondsSinceLastShot > shotCooldownTime)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            secondsSinceLastShot = 0f;
            base.Shoot();
        }
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, tankRotationSpeed * Time.deltaTime);

    }
}
