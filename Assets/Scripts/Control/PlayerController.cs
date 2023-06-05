using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankPawn))]
public class PlayerController : Controller
{
    public KeyCode forwardKeyCode;
    public KeyCode backwardKeyCode;
    public KeyCode leftKeyCode;
    public KeyCode rightKeyCode;
    public KeyCode shootKeyCode;

    private TankPawn playerPawn;

    // Start is called before the first frame update
    public override void Start()
    {
        playerPawn = GetComponent<TankPawn>();

        if (GameManager.Instance)
        {
            GameManager.Instance.players.Add(this);
        }

        base.Start();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.players.Remove(this);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
        base.Update();
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(forwardKeyCode))
        {
            playerPawn.MoveForward();
        }
        if (Input.GetKey(backwardKeyCode))
        {
            playerPawn.MoveBackward();
        }
        if (Input.GetKey(leftKeyCode))
        {
            playerPawn.Rotate(-1f);
        }
        if (Input.GetKey(rightKeyCode))
        {
            playerPawn.Rotate(1f);
        }
        if (Input.GetKeyDown(shootKeyCode))
        {
            playerPawn.Shoot();
        }
    }
}
