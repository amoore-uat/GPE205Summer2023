using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(TankPawn))]
public class PlayerController : Controller
{
    public KeyCode forwardKeyCode;
    public KeyCode backwardKeyCode;
    public KeyCode leftKeyCode;
    public KeyCode rightKeyCode;
    public KeyCode shootKeyCode;
    public KeyCode pauseKeyCode;

    

    // Start is called before the first frame update
    public override void Start()
    {
        pawn = GetComponent<Pawn>();

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
        if (!GameManager.Instance.IsPaused)
        {
            if (Input.GetKey(forwardKeyCode))
            {
                pawn.MoveForward();
            }
            if (Input.GetKey(backwardKeyCode))
            {
                pawn.MoveBackward();
            }
            if (Input.GetKey(leftKeyCode))
            {
                pawn.Rotate(-1f);
            }
            if (Input.GetKey(rightKeyCode))
            {
                pawn.Rotate(1f);
            }
            if (Input.GetKeyDown(shootKeyCode))
            {
                pawn.Shoot();
            }
        }
        if (Input.GetKeyDown(pauseKeyCode))
        {
            GameManager.Instance.TogglePause();
        }
    }
}
