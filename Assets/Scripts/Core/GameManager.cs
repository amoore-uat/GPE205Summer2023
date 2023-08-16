using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateChangedEvent : UnityEvent<GameState, GameState>
{

}
public enum GameState { TitleState, OptionsState, GameplayState, GameOverState, Credits, Pause }

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public GameStateChangedEvent OnGameStateChanged = new GameStateChangedEvent();
    public static GameManager Instance;
    public int numberOfPlayers = 1;
    public List<int> points = new List<int>(); // TODO: Consider moving into Controller
    public List<int> lives = new List<int>();
    public List<Controller> players = new List<Controller>();
    public List<Controller> enemies = new List<Controller>();
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();
    public List<Waypoint> waypoints = new List<Waypoint>();
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public int maxEnemyCount = 4;

    public IEnumerator SpawnTanksNextFrame()
    {
        // Write code here
        yield return null;
        // This code runs on the next frame
        SpawnPlayers();
        SpawnEnemies();
        //SpawnEnemy();
    }

    public IEnumerator SpawnPlayerTankNextFrame()
    {
        // Write code here
        yield return null;
        // This code runs on the next frame
        SpawnPlayer();
    }


    public bool PlayersHaveLives
    {
        get
        {
            int totalLives = 0;
            foreach (int playerLives in lives)
            {
                totalLives += playerLives;
            }
            return (totalLives > 0);
        }
    }

    public int GetPlayerIndex(Pawn source)
    {
        foreach (Controller controller in players)
        {
            if (controller.ControlledPawn == source)
            {
                return (players.IndexOf(controller));
            }
        }

        return -1;
    }

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

    public void SpawnEnemies()
    {
        while (enemies.Count < maxEnemyCount)
        {
            Debug.Log("Enemy Count: " + enemies.Count);
            Debug.Log("Max Number of Enemies: " + maxEnemyCount);
            SpawnEnemy();
        }
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
        //AdjustPlayerCameras();
    }
    public void SpawnEnemy()
    {
        if (pawnSpawnPoints.Count <= (players.Count + enemies.Count))
        {
            Debug.LogError("Need spawn points");
            return;
        }
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        if (spawn.spawnedPawn == null)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawn.transform.position, Quaternion.identity);
            spawn.spawnedPawn = spawnedEnemy.GetComponent<Pawn>();
            enemies.Add(spawnedEnemy.GetComponent<Controller>());
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
        }
        else
        {
            SpawnEnemy();
        }

    }
    public void SpawnPlayer()
    {
        if (pawnSpawnPoints.Count <= numberOfPlayers)
        {
            Debug.LogError("Not enough spawn points");
            return;
        }
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        if (spawn.spawnedPawn == null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
            spawn.spawnedPawn = spawnedPlayer.GetComponent<Pawn>();
            players.Add(spawnedPlayer.GetComponent<Controller>());
            // MAKE SURE THERE ARE ENOUGH PAWN SPAWN POINTS SO THE GAME NEVER BREAKS
            AdjustPlayerCameras();
        }
        else
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayers()
    {
        lives = new List<int>();
        while (players.Count < numberOfPlayers)
        {
            Debug.Log("Player Count: " + players.Count);
            Debug.Log("Number of Players: " + numberOfPlayers);
            SpawnPlayer();
            lives.Add(3);
        }
    }

    public void AdjustPlayerCameras()
    {
        if (players.Count == 1)
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

    public Waypoint GetRandomWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Count)];

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
        StartCoroutine(SpawnTanksNextFrame());
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
