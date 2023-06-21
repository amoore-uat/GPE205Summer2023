using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    private GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;

    private void Start()
    {
        spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    private void Update()
    {
        // If it is there is nothing spawns
        if (spawnedPickup == null)
        {
            // And it is time to spawn
            if (Time.time > nextSpawnTime)
            {
                // Spawn it and set the next time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
