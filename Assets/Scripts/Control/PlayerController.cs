using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankPawn))]
public class PlayerController : Controller
{
    private TankPawn playerPawn;

    // Start is called before the first frame update
    public override void Start()
    {
        playerPawn = GetComponent<TankPawn>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
        base.Update();
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerPawn.MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerPawn.MoveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerPawn.Rotate(-1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerPawn.Rotate(1f);
        }
    }
}
