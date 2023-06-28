using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points = 0;
    public List<Controller> players = new List<Controller>();
    public List<Controller> enemies = new List<Controller>();
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();

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
}
