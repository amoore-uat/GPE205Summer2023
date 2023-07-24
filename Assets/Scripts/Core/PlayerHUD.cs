using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    private Controller controller;
    private int playerIndex;
    public TMP_Text livesText;
    public TMP_Text scoreText;

    private void Start()
    {
        controller = GetComponentInParent<Controller>();
        // TODO: Create a for loop that loops through the player controllers on gamemanger
        // and returns the index that matches.
        UpdateScore();
    }

    public void UpdateScore()
    {
        // TODO: Use the above to finish this
        scoreText.text = "Score: " + GameManager.Instance.points[0];
    }

    public void UpdateLives()
    {

    }
}
