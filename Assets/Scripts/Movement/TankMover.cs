using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody tankRigidBody;

    private void Start()
    {
        tankRigidBody = GetComponent<Rigidbody>();
    }
    public override void Move(float moveSpeed, float direction)
    {
        Vector3 currentPosition = transform.position;
        tankRigidBody.MovePosition(currentPosition + (transform.forward * direction * moveSpeed * Time.deltaTime));
        base.Move(moveSpeed, direction);
    }

    public override void Rotate()
    {
        base.Rotate();
    }
}
