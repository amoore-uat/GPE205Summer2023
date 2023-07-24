using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateChangedEvent : UnityEvent<GameState, GameState>
{

}
public enum GameState { TitleState, OptionsState, GameplayState, GameOverState, Credits, Pause }
public class GameManager : MonoBehaviour
{
    public GameStateChangedEvent OnGameStateChanged = new GameStateChangedEvent();
    public static GameManager Instance;
    public int numberOfPlayers = 2;
    public List<int> points = new List<int>(); // TODO: Consider moving into Controller
    public List<int> lives = new List<int>();
    //public GameObject UIManager; // TODO: Eliminate this variable
    public List<Controller> players = new List<Controller>();
    public List<Controller> enemies = new List<Controller>();
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();

    public bool IsPaused
    {
        get
        {
            return (currentGameState == GameState.Pause);
        }
    }

    public GameState currentGameState = GameState.TitleState;
    private GameState previousGameState;


    public void ChangeGameState(GameState state)
    {
        previousGameState = currentGameState;
        currentGameState = state;
        OnGameStateChanged.Invoke(previousGameState, currentGameState);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attempted to create a second instance of the Game Manager");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        AdjustPlayerCameras();
    }

    void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while (spawn.spawnedPawn != null)
        {
            spawn = GetRandomSpawnPoint();
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }
        // TODO: Make it actually spawn some tanks plz
        // TODO: Refactor this hacky workaround
        AdjustPlayerCameras();
        // Instantiate()
    }

    public void AdjustPlayerCameras()
    {
        if (numberOfPlayers == 1)
        {
            // Get player 1's camera
            Camera player1Camera = players[0].GetComponentInChildren<Camera>();

            // Set player 1's camera location
            Debug.Log(player1Camera.rect);

            // Set player 1's camera location
            player1Camera.rect = new Rect(0, 0, 1f, 1f);

        }
        else
        {
            // Get player 1's camera
            Camera player1Camera = players[0].GetComponentInChildren<Camera>();
            Camera player2Camera = players[1].GetComponentInChildren<Camera>();

            // Set player 1's camera location
            player1Camera.rect = new Rect(0, 0, 0.5f, 1f);
            //Debug.Log(player1Camera.rect);

            // Set player 2's camera location
            player2Camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
    }

    private PawnSpawnPoint GetRandomSpawnPoint()
    {
        return pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Count)];
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void StartGame()
    {
        ChangeGameState(GameState.GameplayState);
        Time.timeScale = 1f;
    }

    public void OpenOptionsMenu()
    {
        ChangeGameState(GameState.OptionsState);
    }

    public void CloseOptionsMenu()
    {
        ChangeGameState(previousGameState);
    }

    public void ChangeToPreviousGameState()
    {
        ChangeGameState(previousGameState);
    }

    public void ChangeStateToTitle()
    {
        ChangeGameState(GameState.TitleState);
    }

    public void PauseGame()
    {
        ChangeGameState(GameState.Pause);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        ChangeGameState(GameState.GameplayState);
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (currentGameState == GameState.Pause)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }
}
