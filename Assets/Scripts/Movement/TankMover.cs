using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    public override void Rotate(float rotationSpeed, float direction)
    {
        float yAngle = direction * Time.deltaTime * rotationSpeed;
        transform.Rotate(0f, yAngle, 0f);
        base.Rotate(rotationSpeed, direction);
    }
}
