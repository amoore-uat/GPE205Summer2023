using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TitleScreenObject;
    public GameObject OptionsMenuObject;
    public GameObject PauseMenuObject;
    public GameObject GameOverObject;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTitleScreenUI()
    {
        TitleScreenObject.SetActive(false);
    }

    public void ShowTitleScreenUI()
    {
        TitleScreenObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        PauseMenuObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenuObject.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        OptionsMenuObject.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        OptionsMenuObject.SetActive(false);
    }

    public void ShowGameOver()
    {

    }

    public void HideGameOver()
    {

    }

    public void HandleGameStateChanged(GameState previousState, GameState newState)
    {
        switch (previousState)
        {
            case GameState.TitleState:
                HideTitleScreenUI();
                break;
            case GameState.OptionsState:
                HideOptionsMenu();
                break;
            case GameState.GameplayState:
                break;
            case GameState.GameOverState:
                HideGameOver();
                break;
            case GameState.Credits:
                // Hide Credits
                break;
            case GameState.Pause:
                HidePauseMenu();
                break;
        }
        switch (newState)
        {
            case GameState.TitleState:
                ShowTitleScreenUI();
                break;
            case GameState.OptionsState:
                ShowOptionsMenu();
                break;
            case GameState.GameplayState:
                break;
            case GameState.GameOverState:
                ShowGameOver();
                break;
            case GameState.Credits:
                break;
            case GameState.Pause:
                ShowPauseMenu();
                break;
        }
    }
}
