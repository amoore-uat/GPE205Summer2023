using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private const float ForwardDirection = 1f;
    private const float BackwardDirection = -1f;
    public float forwardMoveSpeed = 5f;
    public float backwardMoveSpeed = 3f;

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
        Debug.Log("Rotate");
        base.Rotate(direction);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<TankMover>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
