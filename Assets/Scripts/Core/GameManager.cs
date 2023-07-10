using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<int> points = new List<int>(); // TODO: Consider moving into Controller
    public GameObject UIManager; // TODO: Eliminate this variable
    public List<Controller> players = new List<Controller>();
    public List<Controller> enemies = new List<Controller>();
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();


    public enum GameState { TitleState, OptionsState, GameplayState, GameOverState, Credits, Pause }
    public GameState currentGameState = GameState.TitleState;
    private GameState previousGameState;


    public void ChangeGameState(GameState state)
    {
        previousGameState = currentGameState;
        currentGameState = state;
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
        
    }

    void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while (spawn.spawnedPawn != null)
        {
            spawn = GetRandomSpawnPoint();
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }
        // Instantiate()
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
    }

    public void OpenOptionsMenu()
    {
        ChangeGameState(GameState.OptionsState);
        // TODO: Refactor to use a unity event
        if (UIManager)
        {
            UIManager.GetComponent<UIManager>().ShowOptionsMenu();
        }
    }

    public void CloseOptionsMenu()
    {
        ChangeGameState(previousGameState);
    }
}
